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

CREATE TABLE contract_details (
    id                      smallserial PRIMARY KEY,
    accepting_undergraduate boolean,
    accepting_postgraduate  boolean,
    accepting_doctoral      boolean,
    vacancy_max_positions   smallint,
    vacancy_months          smallint,
    conclusion_date         date,
    expiration_date         date
);

CREATE TABLE study_domain (
    id                      smallserial PRIMARY KEY,
    domain_name             varchar(200)
);

CREATE TABLE study_area (
    id                      varchar(4) PRIMARY KEY,
    area_name               varchar(200),
    study_domain_id         smallint REFERENCES study_domain
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

--univeristyDB
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
--study_domain
INSERT INTO study_domain (domain_name) VALUES ('Architecture, Building, Urban and Regional Planning');
INSERT INTO study_domain (domain_name) VALUES ('Law, Business Studies and Management Sciences');
INSERT INTO study_domain (domain_name) VALUES ('Geography, Geology');
INSERT INTO study_domain (domain_name) VALUES ('Engineering, Technology');
INSERT INTO study_domain (domain_name) VALUES ('Mathematics');
INSERT INTO study_domain (domain_name) VALUES ('Informatics');
INSERT INTO study_domain (domain_name) VALUES ('Humanities');
INSERT INTO study_domain (domain_name) VALUES ('Agricultural Sciences');
INSERT INTO study_domain (domain_name) VALUES ('Art and Design');
INSERT INTO study_domain (domain_name) VALUES ('Education, Teacher Training');
INSERT INTO study_domain (domain_name) VALUES ('Natural Sciences');
INSERT INTO study_domain (domain_name) VALUES ('Medical Sciences');
INSERT INTO study_domain (domain_name) VALUES ('Social Sciences');
INSERT INTO study_domain (domain_name) VALUES ('Other Domains of Study');
--study_area
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0520','Engineering and engineering trades','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0525','Motor vehicles, ships and aircraft','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0481','Computer science','6');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0711','Chemical engineering and processes','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0714','Electronics and automation','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0715','Mechanics and metal trades','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0340','Business and Administration','2');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0442','Chemistry','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0710','Engineering and engineering trades','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0730','Architecture and construction','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0421','Biology and biochemistry','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0511','Biology','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0521','Mechanics and metal work','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0713','Electricity and energy','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0700','Engineering, manufacturing and construction','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0610','Information and communication technologies - ICT','6');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0410','Business and Administration','2');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0522','Electricity and energy','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0461','Mathematics','5');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0345','Management and administration','2');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0530','Physical sciences','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0581','Architecture and town planning','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0582','Building and civil engineering','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0850','Environmental protection','3');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0523','Electronics and automation','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0524','Chemical and process','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0580','Architecture and building','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0532','Earth sciences','3');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0541','Mathematics','5');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0732','Building and civil engineering','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0418','Business and Administration','2');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0731','Architecture and town planning','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0533','Physics','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0480','Informatics, computer science','6');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0500','Engineering, manufacturing and construction','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0311','Economics','13');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0716','Motor vehicles, ships and aircraft','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0441','Physics','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0460','Mathematics and statistics','5');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0712','Environmental protection technology','3');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0531','Chemistry','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0400','Artificial Inteligence','6');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0443','Earth sciences','3');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0719','Engineering and engineering trades','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0512','Biochemistry','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0540','Manufacturing and processing','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0611','Computer use','6');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0510','Biological and related sciences','11');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0720','Manufacturing and processing','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0613','Computer use','6');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0724','Mining and extraction','3');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0140','Transport services','14');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0727','Pharmacy','12');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0600','Engineering Technology','4');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0722','Materials','1');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0914','Biomedical Engineering','12');
INSERT INTO study_area (id,area_name,study_domain_id) VALUES ('0413','Management and administration','2');
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
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('A GRAZ109','Fh Joanneum Gesellschaft MBH', 'Austria', 'Graz', 'info@fh-joanneum.at', 'https://www.fh-joanneum.at/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('B ANTWERP01','Universiteit Antwerpen', 'Belgium', 'Antwerp', 'voornaam.achternaam@student.uantwerpen.be', 'https://www.uantwerpen.be/nl/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('B MONS21','Université de Mons', 'Belgium', 'Mons', 'info.mons@umons.ac.be', 'https://web.umons.ac.be/fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('BG SOFIA06','Sofia University SV. Kliment Ohridski', 'Bulgaria', 'Sofia', 'intern@admin.uni-sofia.bg', 'https://www.uni-sofia.bg/eng');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('BG VARNA02','Tehnicheski Universitet-Varna', 'Bulgaria', 'Varna', 'erasmus@tu-varna.bg', 'https://www1.tu-varna.bg/tu-varna/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('CZ BRNO01','Brno University of Technology', 'Czechia', 'Brno', 'vut@vutbr.cz', 'https://www.vut.cz/en/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('CZ LIBEREC01','Technicka Univerzita v Liberci', 'Czechia', 'Liberec', 'info@tul.cz', 'https://www.tul.cz/en/erasmus-2/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('D BERLIN21','SRH Hochschule Berlin', 'Germany', 'Berlin', 'info.hsbe@srh.de', 'https://www.srh-berlin.de/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('D BREMEN04','Hochschule Bremen', 'Germany', 'Bremen', 'zll-support@hs-bremen.de', 'https://www.hs-bremen.de/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('D DARMSTA02','Hochschule Darmstadt', 'Germany', 'Darmstadt', 'helpdesk@h-da.de', 'https://h-da.de/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('E MURCIA01','Universidad de Murcia', 'Spain', 'Murcia', 'siinfo@um.es', 'https://www.um.es/en/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('E TENERIF01','Universidad de la Laguna', 'Spain', 'La Laguna', 'acceso@ull.es', 'https://www.ull.es/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('E SEVILLA01','Universidad de Sevilla', 'Spain', 'Sevilla', 'info@us.es', 'https://www.us.es/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('F MARSEILL84','Universite Aix-Marseille', 'France', 'Marseille', 'info@univ-amu.fr', 'https://www.univ-amu.fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('F NANTES 01','Universite de Nantes', 'France', 'Nantes', 'nom.prenom@univ-nantes.fr', 'https://www.univ-nantes.fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('F LILLE103','Universite de Lille', 'France', 'Lille', 'info@ul.fr', 'https://www.univ-lille.fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('F CHATENA02','Ecole Centrale Paris', 'France', 'Gif-sur-Yvette', 'communication@centralesupelec.fr', 'https://www.centralesupelec.fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('F RENNES01','Universite de Rennes', 'France', 'Rennes', 'info@univ-rennes1.fr', 'https://www.univ-rennes1.fr/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('G KRITIS09','Polytechnio Kritis', 'Greece', 'Chania', 'dpo@tuc.gr', 'https://www.tuc.gr/index.php?id=5397');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('HR RIJEKA01','University of Rijeka', 'Croatia', 'Rijeka', 'ured@uniri.hr', 'https://uniri.hr/en/home/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('I PADOVA01','Universit Delgi Studi di Padova', 'Italy', 'Padova', 'amministrazione.centrale@pec.unipd.it', 'https://www.unipd.it/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('IRL LIMERIC01','University of Limerick', 'Ireland', 'Limerick', 'ulglobal@ul.ie', 'https://www.ul.ie/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('LV RIGA13','BA School of Business and Finance', 'Latvia', 'Riga', 'info@ba.lv', 'https://ba.lv/en/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('P PORTO02','Universidade do Porto', 'Portugal', 'Porto', 'info@up.pt', 'https://www.up.pt/portal/en/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('RO TIMISOA04','Universitatea Politehnica Timisoara', 'Romania', 'Lloyd Palace', 'rector@upt.ro', 'https://www.upt.ro/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('SK ZILINA01','Zlinska Univrzita v Ziline', 'Slovakia', 'Zlin', 'vzdelavanie@uniza.sk', 'https://www.uniza.sk/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('SI LJUBLJA01','University of Ljubijana', 'Slovenia', 'Ljubijana', 'student.office@uni-lj.si', 'https://www.uni-lj.si/university/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('SF ESPOO12','Aalto University', 'Finland', 'Helsinki', 'studentservices@aalto.fi', 'https://www.aalto.fi/en');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('S VAXJO03','Linnaeus University', 'Sweden', 'Växjö', 'admission@lnu.se', 'https://lnu.se/en/');
INSERT INTO university (erasmus_code, name, country, city, email, link) VALUES ('TR ANKARA01','Ankara University', 'Turkey', 'Ankara', 'auprir@ankara.edu.tr', 'https://www.ankara.edu.tr/en/');
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
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'12','6','2014-07-02','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'14','5','2014-07-02','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'8','5','2014-03-21','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'14','10','2014-04-03','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'21','5','2014-04-03','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'20','10','2014-04-03','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'22','5','2017-02-17','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,TRUE,'24','10','2015-11-18','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (FALSE,TRUE,FALSE,'25','4','2015-10-09','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'13','10','2015-10-09','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'12','6','2022-09-02','2022-10-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'17','5','2022-09-02','2021-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'19','10','2022-03-21','2023-02-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'8','5','2022-04-03','2022-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'3','5','2022-04-03','2022-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'14','10','2022-09-30','2023-09-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'20','5','2022-09-02','2023-03-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,TRUE,'24','4','2022-11-18','2023-03-12');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (FALSE,TRUE,FALSE,'11','5','2022-10-02','2023-09-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'6','10','2023-10-02','2024-09-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'18','6','2023-09-02','2023-10-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,FALSE,'3','5','2023-09-02','2024-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'2','10','2023-03-21','2024-02-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'12','5','2023-04-03','2023-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'15','5','2023-04-03','2023-09-30');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'10','10','2023-09-30','2024-09-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'9','5','2023-09-02','2024-03-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,FALSE,TRUE,'12','4','2023-11-18','2024-03-12');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (FALSE,TRUE,FALSE,'10','5','2023-10-02','2024-09-01');
INSERT INTO contract_details (accepting_undergraduate,accepting_postgraduate,accepting_doctoral,vacancy_max_positions,vacancy_months,conclusion_date,expiration_date) VALUES (TRUE,TRUE,FALSE,'7','10','2023-10-02','2024-09-01');
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
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('LV RIGA13','W3','11','0512','21','12');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('B MONS21','W3','12','0727','3','27');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('BG SOFIA06','W7','14','0443','1','23');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('BG VARNA02','W3','15','0914','1','45');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('CZ BRNO01','W13','13','0541','10','29');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('D BREMEN04','W4N','18','0611','2','36');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('D DARMSTA02','W5','20','0522','2','15');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('E MURCIA01','W11','19','0530','4','18');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('E TENERIF01','W4N','17','0480','4','22');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('F NANTES 01','W4N','16','0480','3','37');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('F LILLE103','W2','21','0700','3','14');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('F CHATENA02','W7','28','0850','3','47');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('F RENNES01','W9','30','0140','3','24');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('IRL LIMERIC01','W8N','22','0311','1','17');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('RO TIMISOA04','W11','29','0441','25','38');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('SK ZILINA01','W10','26','0521','28','16');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('SI LJUBLJA01','W1','23','0730','29','8');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('S VAXJO03','W3','27','0531','1','32');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('TR ANKARA01','W4N','24','0481','31','77');
INSERT INTO dest_speciality (dest_university_code,pwr_faculty_short,contract_details_id,study_area_id,subject_language_id,interested_students) VALUES ('TR ANKARA01','W12N','25','0523','31','45');
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
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('10','4.88','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('11','4.83','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('13','4.85','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('14','4.68','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('12','5.04','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('16','5.2','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('17','4.8','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('15','4.85','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('18','5.10','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('19','5.12','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('20','5.18','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('21','4.84','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('23','4.92','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('24','4.98','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('22','4.91','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('26','5.2','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('27','4.78','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('25','4.86','8');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('28','5.09','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('29','5.22','9');
INSERT INTO min_grade_history (dest_speciality_id,grade,semester) VALUES ('30','4.88','9');

