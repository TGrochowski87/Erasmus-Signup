CREATE TABLE status (
    id                      serial PRIMARY KEY,
    content                 varchar(50) NOT NULL
);

CREATE TABLE plan (
    id                      serial PRIMARY KEY,
    student_id              integer NOT NULL,
    speciality_id           smallint NOT NULL,
    status_id               integer REFERENCES status
);


CREATE TABLE subject (
    id                      serial PRIMARY KEY,
    plan_id                 integer REFERENCES plan,
    name                    text NOT NULL,
    ects                    smallint NOT NULL,
    plan_mapping_row        smallint,
    is_pwr                  boolean NOT NULL
);

CREATE TABLE user_home_subject (
    id                      serial PRIMARY KEY,
    student_id              integer NOT NULL,
    name                    text NOT NULL,
    ects                    smallint NOT NULL,
);
