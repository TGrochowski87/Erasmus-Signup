-- na razie typy notatek prawie sie nie roznia,
-- ale do kazdej mozna dodac specyficzny kontent
CREATE TYPE t_note_type AS ENUM (
    'Normal', 'Plan', 'Speciality'
);

CREATE TABLE normal_note (
    id                      serial PRIMARY KEY,
    note_id                 integer REFERENCES note(id),
    content                 text NOT NULL
);

CREATE TABLE plan_note (
    id                      serial PRIMARY KEY,
    note_id                 integer REFERENCES note(id),
    content                 text NOT NULL,
    plan_id                 integer NOT NULL
);

CREATE TABLE speciality_note (
    id                      serial PRIMARY KEY,
    note_id                 integer REFERENCES note(id),
    content                 text NOT NULL,
    speciality_id           integer NOT NULL
);

CREATE TABLE note (
    id                      serial PRIMARY KEY,
    user_id                 integer NOT NULL,
    note_type               t_note_type NOT NULL
);
