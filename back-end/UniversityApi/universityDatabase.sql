CREATE TYPE t_pwr_fac_sh AS ENUM (
    'PWr', 'W1', 'W2', 'W3', 'W4N', 'W5', 'W6', 'W7', 'W8N', 'W9', 'W10', 'W11', 'W12N', 'W13', 'W15', 'F3'
);

CREATE TYPE t_pwr_fac AS ENUM (
    'Architecture',
    'Civil Engineering',
    'Chemistry',
    'Information and Communication Technology',
    'Electrical Engineering',
    'Geoengineering, Mining and Geology',
    'Environmental Engineering',
    'Management',
    'Mechanical and Power Engineering',
    'Mechanical Engineering',
    'Fundamental Problems of Technology',
    'Electronics, Photonics and Microsystems',
    'Pure and Applied Mathematics',
    'University-wide',
    'Off-campus Department WUST'
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
    email                   varchar(100),
    link                    varchar(400)
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
    grade                   real, 
    semester                varchar(30)
);

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

--INSERT_PERSIST_DOCKER_DATA
--subject_language
INSERT INTO subject_language (name) VALUES ('English');
INSERT INTO subject_language (name) VALUES ('German');
INSERT INTO subject_language (name) VALUES ('French');
INSERT INTO subject_language (name) VALUES ('Spanish');
INSERT INTO subject_language (name) VALUES ('Portuguese');
INSERT INTO subject_language (name) VALUES ('Russian');
INSERT INTO subject_language (name) VALUES ('Arabic');
INSERT INTO subject_language (name) VALUES ('Afrikaans');
INSERT INTO subject_language (name) VALUES ('Cantonese');
INSERT INTO subject_language (name) VALUES ('Czech');
INSERT INTO subject_language (name) VALUES ('Danish');
INSERT INTO subject_language (name) VALUES ('Dutch');
INSERT INTO subject_language (name) VALUES ('Finnish');
INSERT INTO subject_language (name) VALUES ('Greek');
INSERT INTO subject_language (name) VALUES ('Hebrew');
INSERT INTO subject_language (name) VALUES ('Hindi');
INSERT INTO subject_language (name) VALUES ('Hungarian');
INSERT INTO subject_language (name) VALUES ('Indonesian');
INSERT INTO subject_language (name) VALUES ('Japanese');
INSERT INTO subject_language (name) VALUES ('Korean');
INSERT INTO subject_language (name) VALUES ('Latvian');
INSERT INTO subject_language (name) VALUES ('Lithuanian');
INSERT INTO subject_language (name) VALUES ('Polish');
INSERT INTO subject_language (name) VALUES ('Norwegian');
INSERT INTO subject_language (name) VALUES ('Romanian');
INSERT INTO subject_language (name) VALUES ('Serbian');
INSERT INTO subject_language (name) VALUES ('Croatian');
INSERT INTO subject_language (name) VALUES ('Slovak');
INSERT INTO subject_language (name) VALUES ('Slovenian');
INSERT INTO subject_language (name) VALUES ('Swedish');
INSERT INTO subject_language (name) VALUES ('Turkish');
INSERT INTO subject_language (name) VALUES ('Ukrainian');
INSERT INTO subject_language (name) VALUES ('Bosnian');
--study_area
INSERT INTO study_area (study_domain, description) VALUES ('0520','Engineering and engineering trades');
INSERT INTO study_area (study_domain, description) VALUES ('0525','Motor vehicles, ships and aircraft');
INSERT INTO study_area (study_domain, description) VALUES ('0481','Computer science');
INSERT INTO study_area (study_domain, description) VALUES ('0711','Chemical engineering and processes');
INSERT INTO study_area (study_domain, description) VALUES ('0714','Electronics and automation');
INSERT INTO study_area (study_domain, description) VALUES ('0715','Mechanics and metal trades');
INSERT INTO study_area (study_domain, description) VALUES ('0340','Business and Administration');
INSERT INTO study_area (study_domain, description) VALUES ('0442','Chemistry');
INSERT INTO study_area (study_domain, description) VALUES ('0710','Engineering and engineering trades');
INSERT INTO study_area (study_domain, description) VALUES ('0730','Architecture and construction');
INSERT INTO study_area (study_domain, description) VALUES ('0421','Biology and biochemistry');
INSERT INTO study_area (study_domain, description) VALUES ('0511','Biology');
INSERT INTO study_area (study_domain, description) VALUES ('0521','Mechanics and metal work');
INSERT INTO study_area (study_domain, description) VALUES ('0713','Electricity and energy');
INSERT INTO study_area (study_domain, description) VALUES ('0700','Engineering, manufacturing and construction');
INSERT INTO study_area (study_domain, description) VALUES ('0610','Information and communication technologies - ICT');
INSERT INTO study_area (study_domain, description) VALUES ('0410','Business and Administration');
INSERT INTO study_area (study_domain, description) VALUES ('0522','Electricity and energy');
INSERT INTO study_area (study_domain, description) VALUES ('0461','Mathematics');
INSERT INTO study_area (study_domain, description) VALUES ('0345','Management and administration');
INSERT INTO study_area (study_domain, description) VALUES ('0530','Physical sciences');
INSERT INTO study_area (study_domain, description) VALUES ('0581','Architecture and town planning');
INSERT INTO study_area (study_domain, description) VALUES ('0582','Building and civil engineering');
INSERT INTO study_area (study_domain, description) VALUES ('0850','Environmental protection (broad programmes)');
INSERT INTO study_area (study_domain, description) VALUES ('0523','Electronics and automation');
INSERT INTO study_area (study_domain, description) VALUES ('0524','Chemical and process');
INSERT INTO study_area (study_domain, description) VALUES ('0580','Architecture and building');
INSERT INTO study_area (study_domain, description) VALUES ('0532','Earth sciences');
INSERT INTO study_area (study_domain, description) VALUES ('0541','Mathematics');
INSERT INTO study_area (study_domain, description) VALUES ('0732','Building and civil engineering');
INSERT INTO study_area (study_domain, description) VALUES ('0418','Business and Administration');
INSERT INTO study_area (study_domain, description) VALUES ('0731','Architecture and town planning');
INSERT INTO study_area (study_domain, description) VALUES ('0533','Physics');
INSERT INTO study_area (study_domain, description) VALUES ('0480','Informatics, computer science');
INSERT INTO study_area (study_domain, description) VALUES ('0500','Engineering, manufacturing and construction/Engineering and engineering trades');
INSERT INTO study_area (study_domain, description) VALUES ('0311','Economics');
INSERT INTO study_area (study_domain, description) VALUES ('0716','Motor vehicles, ships and aircraft');
INSERT INTO study_area (study_domain, description) VALUES ('0441','Physics');
INSERT INTO study_area (study_domain, description) VALUES ('0460','Mathematics and statistics');
INSERT INTO study_area (study_domain, description) VALUES ('0712','Environmental protection technology (07.4 - 85, 850, 851, 859)');
INSERT INTO study_area (study_domain, description) VALUES ('0531','Chemistry');
INSERT INTO study_area (study_domain, description) VALUES ('0400','Mathematics, Informatics');
INSERT INTO study_area (study_domain, description) VALUES ('0443','Earth sciences');
INSERT INTO study_area (study_domain, description) VALUES ('0719','Engineering and engineering trades, not elsewhere classified');
INSERT INTO study_area (study_domain, description) VALUES ('0512','Biochemistry');
INSERT INTO study_area (study_domain, description) VALUES ('0540','Manufacturing and processing');
INSERT INTO study_area (study_domain, description) VALUES ('0611','Computer use');
INSERT INTO study_area (study_domain, description) VALUES ('0510','Biological and related sciences');
INSERT INTO study_area (study_domain, description) VALUES ('0720','Manufacturing and processing');
INSERT INTO study_area (study_domain, description) VALUES ('0613','Computer use');
INSERT INTO study_area (study_domain, description) VALUES ('0724','Mining and extraction');
INSERT INTO study_area (study_domain, description) VALUES ('0140','Transport services');
INSERT INTO study_area (study_domain, description) VALUES ('0727','Pharmacy');
INSERT INTO study_area (study_domain, description) VALUES ('0600','Engineering Technology');
INSERT INTO study_area (study_domain, description) VALUES ('0722','Materials (glass, paper, plastic and wood)');
INSERT INTO study_area (study_domain, description) VALUES ('0914','Biomedical Engineering');
INSERT INTO study_area (study_domain, description) VALUES ('0413','Management and administration');
--pwr_faculty
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W1','Architecture');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W2','Civil Engineering');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W3','Chemistry');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W4N','Information and Communication Technology');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W5','Electrical Engineering');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W6','Geoengineering, Mining and Geology');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W7','Environmental Engineering');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W8N','Management');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W9','Mechanical and Power Engineering');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W10','Mechanical Engineering');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W11','Fundamental Problems of Technology');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W12N','Electronics, Photonics and Microsystems');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W13','Pure and Applied Mathematics');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('PWr','University-wide');
INSERT INTO pwr_faculty (shortcut, name) VALUES ('W15','Off-campus Department WUST');
--university
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('A GRAZ109','FH JOANNEUM GESELLSCHAFT MBH', 'Austria', 'Graz', 'info@fh-joanneum.at', 'https://www.fh-joanneum.at/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('B ANTWERP01','UNIVERSITEIT ANTWERPEN', 'Belgium', 'Antwerp', 'voornaam.achternaam@student.uantwerpen.be', 'https://www.uantwerpen.be/nl/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('CZ LIBEREC01','TECHNICKA UNIVERZITA  V LIBERCI', 'Czechia', 'Liberec', 'info@tul.cz', 'https://www.tul.cz/en/erasmus-2/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('D BERLIN21','SRH HOCHSCHULE BERLIN', 'Germany', 'Berlin', 'info.hsbe@srh.de', 'https://www.srh-berlin.de/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('E SEVILLA01','UNIVERSIDAD DE SEVILLA', 'Spain', 'Sevilla', 'info@us.es', 'https://www.us.es/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('F MARSEILL84','UNIVERSITE AIX-MARSEILLE', 'France', 'Marseille', 'info@univ-amu.fr', 'https://www.univ-amu.fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('G KRITIS09','POLYTECHNIO KRITIS', 'Greece', 'Chania', ' dpo@tuc.gr', 'https://www.tuc.gr/index.php?id=5397');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('HR RIJEKA01','UNIVERSITY OF RIJEKA', 'Croatia', 'Rijeka', 'ured@uniri.hr', 'https://uniri.hr/en/home/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('I PADOVA01','UNIVERSIT DEGLI STUDI DI PADOVA', 'Italy', 'Padova', 'amministrazione.centrale@pec.unipd.it', 'https://www.unipd.it/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('P PORTO02','UNIVERSIDADE DO PORTO', 'Portugal', 'Porto', 'info@up.pt', 'https://www.up.pt/portal/en/');
--pwr_speciality
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IS-II-IO','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IS-II-PSI','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IS-II-ZSTI','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IT-II-IGM','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IT-II-IMT','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IT-II-INS','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IT-II-INT','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IT-II-ISK','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('CBE-II','W4N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('BUD-II-KBU','W2');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('BUD-II-BTO','W2');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('BUD-II-BHS','W2');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IZ-II-ZP','W8N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('IZ-II-BI','W8N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('ZDZ-II-PIP','W8N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('ZDZ-II-ZPP','W8N');
INSERT INTO pwr_speciality (name,pwr_faculty_short) VALUES ('ZDZ-II-ZDM','W8N');
--pwr_subject
INSERT INTO pwr_subject (name,speciality_id,ects) VALUES ('Podstawy biznesu i ochronawłasności intelektualnej','3','3');
INSERT INTO pwr_subject (name,speciality_id,ects) VALUES ('Projekt zespołowy','3','3');
INSERT INTO pwr_subject (name,speciality_id,ects) VALUES ('Praca dyplomowa I','3','2');
INSERT INTO pwr_subject (name,speciality_id,ects) VALUES ('Eksploracja danych metodami uczeniamaszynowego','3','2');
INSERT INTO pwr_subject (name,speciality_id,ects) VALUES ('Projektowanie usług dziedzinowych winfrastrukturze chmurowej','3','2');
INSERT INTO pwr_subject (name,speciality_id,ects) VALUES ('Praca dyplomowa I','1','2');
--contract_details
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'2','6','2014-07-02','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'2','5','2014-07-02','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'2','5','2014-03-21','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'2','10','2014-04-03','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'1','5','2014-04-03','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'2','10','2014-04-03','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'2','5','2017-02-17','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,TRUE,'2','10','2015-11-18','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (FALSE,TRUE,FALSE,'2','4','2015-10-09','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy.max_positions,vacancy.months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'1','10','2015-10-09','2021-09-30');
--dest_speciality
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('A GRAZ109','W4N','1','0610','2','24');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('CZ LIBEREC01','W6','4','0724','10','7');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('F MARSEILL84','W1','2','0730','3','42');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('E SEVILLA01','W8N','8','0345','1','70');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('D BERLIN21','W12N','9','0714','2','28');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('D BERLIN21','W10','10','0521','2','16');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('B ANTWERP01','W2','3','0582','1','55');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('B ANTWERP01','W2','5','0722','1','8');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('P PORTO02','W3','7','0531','1','32');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('I PADOVA01','W4N','6','0481','1','77');
--min_grade_history
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('1','4.8','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('3','4.95','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('4','4.78','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('2','5.1','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('6','5.0','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('7','4.88','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('5','4.8','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('8','5.15','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('9','5.02','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('10','4.68','9');

