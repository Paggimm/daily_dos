module BaseDao

open Npgsql

let get_connection () =
    new NpgsqlConnection("Server=postgres;Port=5432;Database=dailydos;User id=postgres;Password=postgres;")
