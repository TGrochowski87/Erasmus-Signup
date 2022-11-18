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

-- JSON gdy mamy specyficzne wymagania typu wiersz 'x(3,4 rok)' albo 'x(441)' (wiersze 452 i 455)
CREATE TYPE t_pwr_accepting AS ENUM (
    'None', 'All', 'JSON'
);

-- is_json oznacza, ze nalezy sprawdzic vacancy_json
-- mamy przykladowo '4x10 (523), 4x10 (481)', '10x5*' albo '3x9 (3students from each cycle and each programme)'
CREATE TYPE t_vacancy AS (
    max_positions           smallint,
    months                  smallint
);

CREATE TABLE university (
    id                      smallserial PRIMARY KEY,
    erasmus_code            varchar(20) UNIQUE,
    name                    varchar(200) NOT NULL,
    country                 varchar(64) NOT NULL,
    city                    varchar(180) NOT NULL,
    email                   citext,
    link                    citext
);

CREATE TABLE contract (
    id                      smallserial PRIMARY KEY,
    pwr_faculty_short       t_pwr_fac_sh,
    pwr_faculty             t_pwr_fac,
    accepting_undergraduate boolean,
    accepting_postgraduate  boolean,
    accepting_doctoral      boolean,
    undergraduate_year_restriction  varchar(10),
    postgraduate_year_restriction  varchar(10),
    doctoral_year_restriction  varchar(10),
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
    id                      smallint PRIMARY KEY,
    language                varchar(200)
);

CREATE TABLE dest_speciality (
    id                      smallint PRIMARY KEY,
    university_id           smallint REFERENCES university(id),
    contract_id             smallint REFERENCES contract(id),
    study_area_id           smallint REFERENCES study_area(id),
    subject_language_id     smallint REFERENCES subject_language(id),
    interested_students     integer
);

CREATE TABLE min_grade_history (
    id                      serial PRIMARY KEY,
    dest_speciality_id      smallint REFERENCES dest_speciality(id),
    grade                   real, -- double to raczej przesada?
    semester                varchar(30)
);

CREATE TABLE excel_format (
    erasmus_code                    varchar(20),
    university_name                 varchar(220),
    country                         varchar(64),
    city                            varchar(180),
    address                         text,
    pwr_faculty_short               t_pwr_fac_sh,
    pwr_faculty                     t_pwr_fac,
    accepting_undergraduate         t_pwr_accepting,
    accepting_postgraduate          t_pwr_accepting,
    accepting_doctoral              t_pwr_accepting,
    accepting_json                  json,
    undergraduate_year_restriction  varchar(10),
    postgraduate_year_restriction   varchar(10),
    doctoral_year_restriction       varchar(10),
    vacancy                         t_vacancy,
    undergraduate_vacancy           t_vacancy,
    postgraduate_vacancy            t_vacancy,
    doctoral_vacancy                t_vacancy,
    study_domain                    varchar(4)
);

CREATE OR REPLACE FUNCTION handle_excel_row() RETURNS TRIGGER AS $excel_row_insert$
    BEGIN
        INSERT INTO university(erasmus_code, university_name, country, city, address, email, link)
        VALUES (NEW.erasmus_code, NEW.name, NEW.country, NEW.city, NEW.address, NEW.email, NEW.link)
        ON CONFLICT DO NOTHING;

        INSERT INTO university_contract(university_id,
        pwr_faculty_short, pwr_faculty, accepting_undergraduate, accepting_postgraduate, accepting_doctoral, accepting_json, vacancy, vacancy_json, study_domain)
        VALUES (NEW.university_id, NEW.pwr_faculty_short, NEW.pwr_faculty, NEW.accepting_undergraduate, NEW.accepting_postgraduate, NEW.accepting_doctoral, NEW.accepting_json, NEW.vacancy, NEW.vacancy_json, NEW.study_domain);
        
        RETURN NULL;
    END;
$excel_row_insert$ LANGUAGE plpgsql;

CREATE TRIGGER excel_row_insert
    BEFORE INSERT ON excel_format
    FOR EACH ROW
    EXECUTE FUNCTION handle_excel_row();
