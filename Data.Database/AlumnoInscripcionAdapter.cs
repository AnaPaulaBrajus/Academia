using Business.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class AlumnoInscripcionAdapter: Adapter
    {
        public List<AlumnoInscripcion> GetAll()
        {
            List<AlumnoInscripcion> alumnosInscripciones = new List<AlumnoInscripcion>();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from alumnos_inscripciones", this.sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AlumnoInscripcion alumins = new AlumnoInscripcion();
                    alumins.ID = (int)dr["id_inscripcion"];
                    alumins.IdCurso = (int)dr["id_curso"];
                    alumins.IdAlumno = (int)dr["id_alumno"];
                    alumins.Condicion = (string)dr["condicion"];
                    alumins.Nota = (int)dr["nota"];
                    alumnosInscripciones.Add(alumins);
                }
                dr.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                    new Exception("Error al recuperar lista de inscripciones de alumnos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return alumnosInscripciones;
        }

        public List<AlumnoInscripcion> GetInscAlumno(int ID)
        {
            List<AlumnoInscripcion> alumnosInscripciones = new List<AlumnoInscripcion>();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from alumnos_inscripciones where id_alumno = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AlumnoInscripcion alumins = new AlumnoInscripcion();
                    alumins.ID = (int)dr["id_inscripcion"];
                    alumins.IdCurso = (int)dr["id_curso"];
                    alumins.IdAlumno = (int)dr["id_alumno"];
                    alumins.Condicion = (string)dr["condicion"];
                    alumins.Nota = (int)dr["nota"];
                    alumnosInscripciones.Add(alumins);
                }
                dr.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de inscripciones de alumnos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return alumnosInscripciones;
        }

        public AlumnoInscripcion GetOne(int ID)
        {
            AlumnoInscripcion alumins = new AlumnoInscripcion();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from alumnos_inscripciones where id_inscripcion = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    alumins.ID = (int)dr["id_inscripcion"];
                    alumins.IdCurso = (int)dr["id_curso"];
                    alumins.IdAlumno = (int)dr["id_alumno"];
                    alumins.Condicion = (string)dr["condicion"];
                    alumins.Nota = (int)dr["nota"];
                    dr.Close();
                }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar la inscripcion del alumno", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return alumins;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("delete alumnos_inscripciones where id_inscripcion = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar la inscripcion del alumno", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(AlumnoInscripcion alumins)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE alumnos_inscripciones SET id_alumno= @id_alumno, id_curso = @id_curso " +
                    "WHERE id_inscripcion = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = alumins.ID;
                cmd.Parameters.Add("@id_alumno", SqlDbType.Int).Value = alumins.IdAlumno;
                cmd.Parameters.Add("@id_curso", SqlDbType.Int).Value = alumins.IdCurso;
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos de la inscripcion del alumno", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Insert(AlumnoInscripcion alumins)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO alumnos_inscripciones(id_alumno, id_curso, condicion, nota) " +
                                                "values(@id_alumno, @id_curso, @condicion, @nota) " +
                                                "select @@identity", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = alumins.ID;
                cmd.Parameters.Add("@id_alumno", SqlDbType.Int).Value = alumins.IdAlumno;
                cmd.Parameters.Add("@id_curso", SqlDbType.Int).Value = alumins.IdCurso;
                cmd.Parameters.Add("@condicion", SqlDbType.VarChar).Value = alumins.Condicion;
                cmd.Parameters.Add("@nota", SqlDbType.Int).Value = alumins.Nota;
                alumins.ID = Decimal.ToInt32((decimal)cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear la inscripcion del alumno", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(AlumnoInscripcion alumins)
        {
            if (alumins.State == BusinessEntity.States.Deleted)
            {
                this.Delete(alumins.ID);
            }
            else if (alumins.State == BusinessEntity.States.New)
            {
                this.Insert(alumins);
            }
            else if (alumins.State == BusinessEntity.States.Modified)
            {
                this.Update(alumins);
            }
            alumins.State = BusinessEntity.States.Unmodified;
        }

        public DataTable GetCursos(int id)
        {
            DataTable cursos = new DataTable();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from cursos where id_curso not in " +
                    "(select id_curso from alumnos_inscripciones where id_alumno = @id)", sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(cursos);
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de cursos", Ex);
                throw Ex;
            }
            finally
            {
                this.CloseConnection();
            }
            return cursos;
        }

        public DataTable GetAlumnos()
        {
            DataTable alumnos = new DataTable();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select id_persona,concat( nombre,' ', apellido)apenom from personas where tipo_persona=0", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(alumnos);
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de alumnos", Ex);
                throw Ex;
            }
            finally
            {
                this.CloseConnection();
            }
            return alumnos;
        }

        public List<AlumnoInscripcion> GetAlumnosCurso(int idCurso)
        {
            List<AlumnoInscripcion> alumnosCurso = new List<AlumnoInscripcion>();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from alumnos_inscripciones where id_curso = @idCurso", this.sqlConn);
                cmd.Parameters.Add("@idCurso", SqlDbType.Int).Value = idCurso;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AlumnoInscripcion alumCurso = new AlumnoInscripcion();
                    alumCurso.ID = (int)dr["id_inscripcion"];
                    alumCurso.IdAlumno = (int)dr["id_alumno"];
                    alumCurso.IdCurso = (int)dr["id_curso"];
                    alumCurso.Condicion = (string)dr["condicion"];
                    alumCurso.Nota = (int)dr["nota"];
                    alumnosCurso.Add(alumCurso);
                }
                dr.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                    new Exception("Error al recuperar lista de alumnos de ese curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return alumnosCurso;
        }
    }
}
