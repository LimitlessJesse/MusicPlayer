using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicPlayer.Migrations
{
    /// <inheritdoc />
    public partial class RemovemodelBuilderforPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Playlists_PlaylistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_PlaylistId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "PlaylistSong",
                columns: table => new
                {
                    PlaylistsPlaylistId = table.Column<int>(type: "int", nullable: false),
                    SongsSourceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SongsUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSong", x => new { x.PlaylistsPlaylistId, x.SongsSourceId, x.SongsUserId });
                    table.ForeignKey(
                        name: "FK_PlaylistSong_Playlists_PlaylistsPlaylistId",
                        column: x => x.PlaylistsPlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSong_Songs_SongsSourceId_SongsUserId",
                        columns: x => new { x.SongsSourceId, x.SongsUserId },
                        principalTable: "Songs",
                        principalColumns: new[] { "SourceId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSong_SongsSourceId_SongsUserId",
                table: "PlaylistSong",
                columns: new[] { "SongsSourceId", "SongsUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistSong");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_PlaylistId",
                table: "Songs",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Playlists_PlaylistId",
                table: "Songs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "PlaylistId");
        }
    }
}
