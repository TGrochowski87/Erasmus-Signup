using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniversityApi.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:t_pwr_fac", "Ogólnouczelniana,Architektury,Budownictwa,Chemiczny,Informatyki i Telekomunikacji,Elektryczny,Geoinżynierii, Górnictwa i Geologii,Inżynierii Środowiska,Zarządzania,Mechaniczno-Energetyczny,Mechaniczny,Podstawowych Problemów Techniki,Elektroniki Fotoniki i Mikrosystemów,Matematyki,Wydział Zamiejscowy PWr,Techniczno-Inżynieryjny ZOD")
                .Annotation("Npgsql:Enum:t_pwr_fac_sh", "PWr,W1,W2,W3,W4N,W5,W6,W7,W8N,W9,W10,W11,W12N,W13,W15,F3")
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "contract_details",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    acceptingundergraduate = table.Column<bool>(name: "accepting_undergraduate", type: "boolean", nullable: true),
                    acceptingpostgraduate = table.Column<bool>(name: "accepting_postgraduate", type: "boolean", nullable: true),
                    acceptingdoctoral = table.Column<bool>(name: "accepting_doctoral", type: "boolean", nullable: true),
                    conclusiondate = table.Column<DateTime>(name: "conclusion_date", type: "timestamp with time zone", nullable: true),
                    expirationdate = table.Column<DateTime>(name: "expiration_date", type: "timestamp with time zone", nullable: true),
                    vacancymaxpositions = table.Column<int>(name: "vacancy_max_positions", type: "integer", nullable: true),
                    vacancymonths = table.Column<int>(name: "vacancy_months", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_details", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pwr_faculty",
                columns: table => new
                {
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "pwr_speciality",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pwr_speciality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "study_area",
                columns: table => new
                {
                    studydomain = table.Column<string>(name: "study_domain", type: "character varying(4)", maxLength: 4, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("study_area_pkey", x => x.studydomain);
                });

            migrationBuilder.CreateTable(
                name: "subject_language",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject_language", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "university",
                columns: table => new
                {
                    erasmuscode = table.Column<string>(name: "erasmus_code", type: "character varying(30)", maxLength: 30, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    country = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    city = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: true),
                    email = table.Column<string>(type: "citext", nullable: true),
                    link = table.Column<string>(type: "citext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("university_pkey", x => x.erasmuscode);
                });

            migrationBuilder.CreateTable(
                name: "pwr_subject",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    specialityid = table.Column<int>(name: "speciality_id", type: "integer", nullable: true),
                    ects = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pwr_subject", x => x.id);
                    table.ForeignKey(
                        name: "pwr_subject_speciality_id_fkey",
                        column: x => x.specialityid,
                        principalTable: "pwr_speciality",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "dest_speciality",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    destuniversitycode = table.Column<string>(name: "dest_university_code", type: "character varying(30)", maxLength: 30, nullable: true),
                    contractdetailsid = table.Column<short>(name: "contract_details_id", type: "smallint", nullable: true),
                    studyareaid = table.Column<string>(name: "study_area_id", type: "character varying(4)", maxLength: 4, nullable: true),
                    subjectlanguageid = table.Column<short>(name: "subject_language_id", type: "smallint", nullable: true),
                    interestedstudents = table.Column<int>(name: "interested_students", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dest_speciality", x => x.id);
                    table.ForeignKey(
                        name: "dest_speciality_contract_details_id_fkey",
                        column: x => x.contractdetailsid,
                        principalTable: "contract_details",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "dest_speciality_dest_university_code_fkey",
                        column: x => x.destuniversitycode,
                        principalTable: "university",
                        principalColumn: "erasmus_code");
                    table.ForeignKey(
                        name: "dest_speciality_study_area_id_fkey",
                        column: x => x.studyareaid,
                        principalTable: "study_area",
                        principalColumn: "study_domain");
                    table.ForeignKey(
                        name: "dest_speciality_subject_language_id_fkey",
                        column: x => x.subjectlanguageid,
                        principalTable: "subject_language",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "min_grade_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    destspecialityid = table.Column<short>(name: "dest_speciality_id", type: "smallint", nullable: true),
                    grade = table.Column<float>(type: "real", nullable: true),
                    semester = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_min_grade_history", x => x.id);
                    table.ForeignKey(
                        name: "min_grade_history_dest_speciality_id_fkey",
                        column: x => x.destspecialityid,
                        principalTable: "dest_speciality",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_dest_speciality_contract_details_id",
                table: "dest_speciality",
                column: "contract_details_id");

            migrationBuilder.CreateIndex(
                name: "IX_dest_speciality_dest_university_code",
                table: "dest_speciality",
                column: "dest_university_code");

            migrationBuilder.CreateIndex(
                name: "IX_dest_speciality_study_area_id",
                table: "dest_speciality",
                column: "study_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_dest_speciality_subject_language_id",
                table: "dest_speciality",
                column: "subject_language_id");

            migrationBuilder.CreateIndex(
                name: "IX_min_grade_history_dest_speciality_id",
                table: "min_grade_history",
                column: "dest_speciality_id");

            migrationBuilder.CreateIndex(
                name: "IX_pwr_subject_speciality_id",
                table: "pwr_subject",
                column: "speciality_id");

            migrationBuilder.CreateIndex(
                name: "subject_language_name_key",
                table: "subject_language",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "min_grade_history");

            migrationBuilder.DropTable(
                name: "pwr_faculty");

            migrationBuilder.DropTable(
                name: "pwr_subject");

            migrationBuilder.DropTable(
                name: "dest_speciality");

            migrationBuilder.DropTable(
                name: "pwr_speciality");

            migrationBuilder.DropTable(
                name: "contract_details");

            migrationBuilder.DropTable(
                name: "university");

            migrationBuilder.DropTable(
                name: "study_area");

            migrationBuilder.DropTable(
                name: "subject_language");
        }
    }
}
