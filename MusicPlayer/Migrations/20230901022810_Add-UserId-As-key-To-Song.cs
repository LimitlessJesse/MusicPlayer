using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicPlayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdAskeyToSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Songs_SongsSourceId",
                table: "PlaylistSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Songs",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistSong_SongsSourceId",
                table: "PlaylistSong");

            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                table: "Songs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Songs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "SongsUserId",
                table: "PlaylistSong",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Songs",
                table: "Songs",
                columns: new[] { "SourceId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong",
                columns: new[] { "PlaylistsPlaylistId", "SongsSourceId", "SongsUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_UserId",
                table: "Songs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSong_SongsSourceId_SongsUserId",
                table: "PlaylistSong",
                columns: new[] { "SongsSourceId", "SongsUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Songs_SongsSourceId_SongsUserId",
                table: "PlaylistSong",
                columns: new[] { "SongsSourceId", "SongsUserId" },
                principalTable: "Songs",
                principalColumns: new[] { "SourceId", "UserId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_AspNetUsers_UserId",
                table: "Songs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Songs_SongsSourceId_SongsUserId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_AspNetUsers_UserId",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Songs",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_UserId",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong");

            migrationBuilder.DropIndex(
                name: "IX_PlaylistSong_SongsSourceId_SongsUserId",
                table: "PlaylistSong");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongsUserId",
                table: "PlaylistSong");

            migrationBuilder.AlterColumn<string>(
                name: "SourceId",
                table: "Songs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Songs",
                table: "Songs",
                column: "SourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong",
                columns: new[] { "PlaylistsPlaylistId", "SongsSourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSong_SongsSourceId",
                table: "PlaylistSong",
                column: "SongsSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Songs_SongsSourceId",
                table: "PlaylistSong",
                column: "SongsSourceId",
                principalTable: "Songs",
                principalColumn: "SourceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
