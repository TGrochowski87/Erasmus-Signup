CREATE EXTENSION citext;

CREATE TYPE t_pwr_fac_sh AS ENUM (
    'PWr', 'W1', 'W2', 'W3', 'W4N', 'W5', 'W6', 'W7', 'W8N', 'W9', 'W10', 'W11', 'W12N', 'W13', 'W15', 'F3'
);

CREATE TYPE t_pwr_fac AS ENUM (
    'Ogólnouczelniana',
    'Architektury',
    'Budownictwa',
    'Chemiczny',
    'Informatyki i Telekomunikacji',
    'Elektryczny',
    'Geoinżynierii, Górnictwa i Geologii',
    'Inżynierii Środowiska',
    'Zarządzania',
    'Mechaniczno-Energetyczny',
    'Mechaniczny',
    'Podstawowych Problemów Techniki',
    'Elektroniki Fotoniki i Mikrosystemów',
    'Matematyki',
    'Wydział Zamiejscowy PWr',
    'Techniczno-Inżynieryjny ZOD'
);

CREATE TYPE t_vacancy AS (
    max_positions           smallint,
    months                  smallint
);

CREATE TABLE university (
    erasmus_code            varchar(30) PRIMARY KEY,
    name                    varchar(200) NOT NULL,
    country                 varchar(64),
    city                    varchar(180),
    email                   citext,
    link                    citext
);

CREATE TABLE pwr_faculty (
    shortcut                t_pwr_fac_sh PRIMARY KEY,
    name                    t_pwr_fac
);

CREATE TABLE contract_specific_details (
    id                      smallserial PRIMARY KEY,
    undergraduate_vacancy   t_vacancy,
    postgraduate_vacancy    t_vacancy,
    doctoral_vacancy        t_vacancy,
    undergraduate_year_restriction  varchar(10),
    postgraduate_year_restriction   varchar(10),
    doctoral_year_restriction       varchar(10),
    is_aggregate            boolean
);

CREATE TABLE contract_details (
    id                      smallserial PRIMARY KEY,
    accepting_undergraduate boolean,
    accepting_postgraduate  boolean,
    accepting_doctoral      boolean,
    vacancy                 t_vacancy,
    conclusion_date         date,
    expiration_date         date,
    specific_details_id     smallint REFERENCES contract_specific_details
);

CREATE TABLE study_area (
    study_domain            varchar(4) PRIMARY KEY,
    description             varchar(200)
);

CREATE TABLE subject_language (
    id                      smallserial PRIMARY KEY,
    name                    varchar(200) UNIQUE
);

CREATE TABLE dest_speciality (
    id                      smallserial PRIMARY KEY,
    dest_university_code    varchar(30) REFERENCES university,
    pwr_faculty_short       t_pwr_fac_sh REFERENCES pwr_faculty,
    contract_details_id     smallint REFERENCES contract_details,
    study_area_id           varchar(4) REFERENCES study_area,
    subject_language_id     smallint REFERENCES subject_language,
    interested_students     integer
);

CREATE TABLE min_grade_history (
    id                      serial PRIMARY KEY,
    dest_speciality_id      smallint REFERENCES dest_speciality,
    grade                   real, -- double to raczej przesada?
    semester                varchar(30)
);

CREATE TABLE excel_format (
    erasmus_code            varchar(30),
    country                 varchar(60),
    university_name         varchar(200),
    pwr_faculty_short       t_pwr_fac_sh,
    pwr_faculty             t_pwr_fac,
    accepting_undergraduate boolean,
    undergraduate_year_restriction  varchar(10),
    accepting_postgraduate  boolean,
    postgraduate_year_restriction   varchar(10),
    accepting_doctoral      boolean,
    doctoral_year_restriction       varchar(10),
    vacancy                 t_vacancy,
    study_domain            varchar(4),
    area_description        varchar(200),
    sub_language            varchar(200),
    conclusion_date         date,
    expiration_date         date
);

CREATE OR REPLACE FUNCTION handle_excel_row() RETURNS TRIGGER AS $excel_row_insert$
    DECLARE
        contract_id smallint;
        language_id smallint;
        language    varchar(200);
        details_id smallint;
    BEGIN
        INSERT INTO pwr_faculty(shortcut, name)
        VALUES (NEW.pwr_faculty_short, NEW.pwr_faculty)
        ON CONFLICT DO NOTHING;

        INSERT INTO university(erasmus_code, name, country)
        VALUES (NEW.erasmus_code, NEW.university_name, NEW.country)
        ON CONFLICT DO NOTHING;

        INSERT INTO contract_details(accepting_undergraduate, accepting_postgraduate, accepting_doctoral, vacancy, conclusion_date, expiration_date, specific_details_id)
        VALUES (NEW.accepting_undergraduate, NEW.accepting_postgraduate, NEW.accepting_doctoral, NEW.vacancy, NEW.conclusion_date, NEW.expiration_date, details_id)
        RETURNING id INTO contract_id;

        INSERT INTO study_area(study_domain, description)
        VALUES (NEW.study_domain, NEW.area_description)
        ON CONFLICT (study_domain) DO NOTHING;

        IF NEW.sub_language IS NULL THEN
            language := 'Angielski';
        END IF;
        
        INSERT INTO subject_language(name)
        VALUES (language)
        ON CONFLICT (name)
        DO UPDATE SET name=EXCLUDED.name
        RETURNING id INTO language_id;

        INSERT INTO dest_speciality(dest_university_code, pwr_faculty_short, contract_details_id, study_area_id, subject_language_id, interested_students)
        VALUES (NEW.erasmus_code, NEW.pwr_faculty_short, contract_id, NEW.study_domain, language_id, 0);
        
        RETURN NULL;
    END;
$excel_row_insert$ LANGUAGE plpgsql;

CREATE TRIGGER excel_row_insert
    BEFORE INSERT ON excel_format
    FOR EACH ROW
    EXECUTE FUNCTION handle_excel_row();

CREATE TABLE pwr_speciality (
    id                      serial PRIMARY KEY,
    name                    varchar(200) NOT NULL,
    pwr_faculty_short       t_pwr_fac_sh REFERENCES pwr_faculty
);

CREATE TABLE pwr_subject (
    id                      serial PRIMARY KEY,
    name                    varchar(200) NOT NULL,
    speciality_id           integer REFERENCES pwr_speciality,
    ects                    integer NOT NULL
);

SET datestyle = 'dmy';
\copy excel_format FROM '/usr/local/etc/data.csv' DELIMITER ';' CSV
