CREATE TABLE opinion (
    id                      serial PRIMARY KEY,
    student_id              integer NOT NULL,
    speciality_id           smallint NOT NULL,
    content                 text NOT NULL,
    rating                  float NOT NULL
);
