using Microsoft.EntityFrameworkCore.Migrations;

namespace Sicpa.Api.Application.Migrations
{
    public partial class MigrationPostgress_v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_id_department",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Enterprises_id_enterprise",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsEmpoyees_Departments_departmentid",
                table: "DepartmentsEmpoyees");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsEmpoyees_Employees_employeeid",
                table: "DepartmentsEmpoyees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_id_employee",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enterprises",
                table: "Enterprises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentsEmpoyees",
                table: "DepartmentsEmpoyees");

            migrationBuilder.RenameTable(
                name: "Enterprises",
                newName: "enterprises");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employees");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "departments");

            migrationBuilder.RenameTable(
                name: "DepartmentsEmpoyees",
                newName: "departments_employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_id_employee",
                table: "employees",
                newName: "IX_employees_id_employee");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_id_enterprise",
                table: "departments",
                newName: "IX_departments_id_enterprise");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_id_department",
                table: "departments",
                newName: "IX_departments_id_department");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentsEmpoyees_employeeid",
                table: "departments_employees",
                newName: "IX_departments_employees_employeeid");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentsEmpoyees_departmentid",
                table: "departments_employees",
                newName: "IX_departments_employees_departmentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_enterprises",
                table: "enterprises",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employees",
                table: "employees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments_employees",
                table: "departments_employees",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_departments_id_department",
                table: "departments",
                column: "id_department",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_enterprises_id_enterprise",
                table: "departments",
                column: "id_enterprise",
                principalTable: "enterprises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_employees_departments_departmentid",
                table: "departments_employees",
                column: "departmentid",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_employees_employees_employeeid",
                table: "departments_employees",
                column: "employeeid",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_employees_id_employee",
                table: "employees",
                column: "id_employee",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_departments_id_department",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_enterprises_id_enterprise",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_employees_departments_departmentid",
                table: "departments_employees");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_employees_employees_employeeid",
                table: "departments_employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_employees_id_employee",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_enterprises",
                table: "enterprises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employees",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments_employees",
                table: "departments_employees");

            migrationBuilder.RenameTable(
                name: "enterprises",
                newName: "Enterprises");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "departments_employees",
                newName: "DepartmentsEmpoyees");

            migrationBuilder.RenameIndex(
                name: "IX_employees_id_employee",
                table: "Employees",
                newName: "IX_Employees_id_employee");

            migrationBuilder.RenameIndex(
                name: "IX_departments_id_enterprise",
                table: "Departments",
                newName: "IX_Departments_id_enterprise");

            migrationBuilder.RenameIndex(
                name: "IX_departments_id_department",
                table: "Departments",
                newName: "IX_Departments_id_department");

            migrationBuilder.RenameIndex(
                name: "IX_departments_employees_employeeid",
                table: "DepartmentsEmpoyees",
                newName: "IX_DepartmentsEmpoyees_employeeid");

            migrationBuilder.RenameIndex(
                name: "IX_departments_employees_departmentid",
                table: "DepartmentsEmpoyees",
                newName: "IX_DepartmentsEmpoyees_departmentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enterprises",
                table: "Enterprises",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentsEmpoyees",
                table: "DepartmentsEmpoyees",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_id_department",
                table: "Departments",
                column: "id_department",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Enterprises_id_enterprise",
                table: "Departments",
                column: "id_enterprise",
                principalTable: "Enterprises",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsEmpoyees_Departments_departmentid",
                table: "DepartmentsEmpoyees",
                column: "departmentid",
                principalTable: "Departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsEmpoyees_Employees_employeeid",
                table: "DepartmentsEmpoyees",
                column: "employeeid",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_id_employee",
                table: "Employees",
                column: "id_employee",
                principalTable: "Employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
