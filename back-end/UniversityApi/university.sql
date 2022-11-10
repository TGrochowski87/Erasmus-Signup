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
    positions   smallint,
    months      smallint,
    is_json     boolean
);

CREATE TABLE university_contract (
    id                      smallserial,
    erasmus_code            varchar(20),
    university_name         varchar(220),
    pwr_faculty_short       t_pwr_fac_sh,
    pwr_faculty             t_pwr_fac,
    accepting_undergraduate t_pwr_accepting,
    accepting_postgraduate  t_pwr_accepting,
    accepting_doctoral      t_pwr_accepting,
    accepting_json          json,
    vacancy                 t_vacancy,
    vacancy_json            json,
    study_domain            varchar(4)
);
