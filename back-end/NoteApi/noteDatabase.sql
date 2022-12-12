-- na razie typy notatek prawie sie nie roznia,
-- ale do kazdej mozna dodac specyficzny kontent
CREATE TYPE t_note_type AS ENUM (
    'Normal', 'Plan', 'Speciality'
);

CREATE TABLE note (
    id                      serial PRIMARY KEY,
    user_id                 integer NOT NULL,
    created_at              timestamp NOT NULL
);

CREATE TABLE normal_note (
    id                      serial PRIMARY KEY,
    note_id                 integer REFERENCES note(id),
    content                 text NOT NULL
);

CREATE TABLE plan_note (
    note_id                 integer REFERENCES note(id) ON DELETE CASCADE,
    plan_id                 integer NOT NULL,
    content                 text NOT NULL,
    PRIMARY KEY (note_id)
);

CREATE TABLE speciality_note (
    note_id                 integer REFERENCES note(id) ON DELETE CASCADE,
    speciality_id           integer NOT NULL,
    content                 text NOT NULL,
    PRIMARY KEY (note_id)
);

CREATE TABLE speciality_highlight_note (
    note_id                 integer REFERENCES note(id) ON DELETE CASCADE,
    speciality_id           integer NOT NULL,
    positive                boolean NOT NULL, -- true -> like, false -> dislike/blacklist
    PRIMARY KEY (note_id)
);

CREATE TABLE speciality_priority_note (
    note_id                 integer REFERENCES note(id) ON DELETE CASCADE,
    speciality_id           integer NOT NULL,
    priority                smallint NOT NULL,
    CONSTRAINT valid_priority CHECK ((1 <= priority) AND (priority <= 10)),
    PRIMARY KEY (note_id)
);
