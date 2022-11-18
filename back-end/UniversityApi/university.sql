CREATE EXTENSION citext;

CREATE TYPE t_pwr_fac_sh AS ENUM (
    'PWR', 'W1', 'W2', 'W3', 'W4N', 'W5', 'W6', 'W7', 'W8N', 'W9', 'W10', 'W11', 'W12N', 'W13', 'W15', 'F3'
);

CREATE TYPE t_pwr_fac AS ENUM (
    'PWR',
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
    'Elektroniki Mikrosystemów i Fotoniki',
    'Matematyki',
    'Wydział Zamiejscowy PWr',
    'Techniczno-Inżynieryjny ZOD'
);

CREATE TYPE t_vacancy AS (
    max_positions           smallint,
    months                  smallint
);

CREATE TABLE university (
    erasmus_code            varchar(20) PRIMARY KEY,
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

CREATE TABLE contract_details (
    id                      smallserial PRIMARY KEY,
    accepting_undergraduate boolean,
    accepting_postgraduate  boolean,
    accepting_doctoral      boolean,
    undergraduate_year_restriction  varchar(10),
    postgraduate_year_restriction   varchar(10),
    doctoral_year_restriction       varchar(10),
    vacancy                 t_vacancy,
    undergraduate_vacancy   t_vacancy,
    postgraduate_vacancy    t_vacancy,
    doctoral_vacancy        t_vacancy
);

CREATE TABLE study_area (
    study_domain            varchar(4) PRIMARY KEY,
    description             varchar(200)
);

CREATE TABLE subject_language (
    id                      smallserial PRIMARY KEY,
    language                varchar(200) UNIQUE
);

CREATE TABLE dest_speciality (
    id                      smallserial PRIMARY KEY,
    dest_university_code    varchar(20) REFERENCES university,
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
    erasmus_code            varchar(20),
    university_name         varchar(200),
    pwr_faculty_short       t_pwr_fac_sh,
    pwr_faculty             t_pwr_fac,
    accepting_undergraduate boolean,
    accepting_postgraduate  boolean,
    accepting_doctoral      boolean,
    undergraduate_year_restriction  varchar(10),
    postgraduate_year_restriction   varchar(10),
    doctoral_year_restriction       varchar(10),
    vacancy                 t_vacancy,
    undergraduate_vacancy   t_vacancy,
    postgraduate_vacancy    t_vacancy,
    doctoral_vacancy        t_vacancy,
    study_domain            varchar(4),
    area_description        varchar(200),
    language                varchar(200)
);

CREATE OR REPLACE FUNCTION handle_excel_row() RETURNS TRIGGER AS $excel_row_insert$
    DECLARE
        contract_id smallint;
        language_id smallint;
    BEGIN
        INSERT INTO pwr_faculty(shortcut, name)
        VALUES (NEW.pwr_faculty_short, NEW.pwr_faculty)
        ON CONFLICT DO NOTHING;

        INSERT INTO university(erasmus_code, name)
        VALUES (NEW.erasmus_code, NEW.university_name)
        ON CONFLICT DO NOTHING;

        INSERT INTO contract_details(accepting_undergraduate, accepting_postgraduate, accepting_doctoral, undergraduate_year_restriction, postgraduate_year_restriction, doctoral_year_restriction, vacancy, undergraduate_vacancy, postgraduate_vacancy, doctoral_vacancy)
        VALUES (NEW.accepting_undergraduate, NEW.accepting_postgraduate, NEW.accepting_doctoral, NEW.undergraduate_year_restriction, NEW.postgraduate_year_restriction, NEW.doctoral_year_restriction, NEW.vacancy, NEW.undergraduate_vacancy, NEW.postgraduate_vacancy, NEW.doctoral_vacancy)
        RETURNING id INTO contract_id;

        INSERT INTO study_area(study_domain, description)
        VALUES (NEW.study_domain, NEW.area_description)
        ON CONFLICT (study_domain) DO NOTHING;

        INSERT INTO subject_language(language)
        VALUES (NEW.language)
        ON CONFLICT (language)
        DO UPDATE SET language=EXCLUDED.language
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
