CREATE TABLE plan (
    id                      serial PRIMARY KEY,
    student_id              bigint NOT NULL,
    name                    text NOT NULL
);

CREATE TABLE home_subject (
    id                      serial PRIMARY KEY,
    student_id              bigint NOT NULL,
    name                    text NOT NULL,
    ects                    smallint NOT NULL
);

CREATE TABLE subject (
    id                      serial PRIMARY KEY,
    plan_id                 integer REFERENCES plan ON DELETE CASCADE,
    mapped_subject          integer REFERENCES home_subject ON DELETE CASCADE,
    name                    text NOT NULL,
    ects                    smallint NOT NULL
);
