using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using System.Data.SqlClient;
using System.Data;

namespace Data.Database
{
    public class DocenteCursoAdapter : Adapter
    {
        public List<DocenteCurso> GetAll()
        {
            List<DocenteCurso> docentesCursos = new List<DocenteCurso>();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from docentes_cursos", this.sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DocenteCurso docur = new DocenteCurso();
                    docur.ID = (int)dr["id_dictado"];
                    docur.IdCurso = (int)dr["id_curso"];
                    docur.IdDocente = (int)dr["id_docente"];
                    docur.Cargo = (DocenteCurso.TipoCargo)dr["cargo"];
                    docentesCursos.Add(docur);
                }
                dr.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                    new Exception("Error al recuperar lista de cursos de docentes", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return docentesCursos;
        }

        public DocenteCurso GetOne(int ID)
        {
            DocenteCurso docur = new DocenteCurso();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from docentes_cursos where id_dictado = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    docur.ID = (int)dr["id_dictado"];
                    docur.IdCurso = (int)dr["id_curso"];
                    docur.IdDocente = (int)dr["id_docente"];
                    docur.Cargo = (DocenteCurso.TipoCargo)dr["cargo"];
                    dr.Close();
                }
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar el docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return docur;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("delete docentes_cursos where id_dictado = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al eliminar el docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Update(DocenteCurso docur)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE docentes_cursos SET id_curso = @id_curso, id_docente= @id_docente, cargo=@cargo " +
                    "WHERE id_dictado = @id", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = docur.ID;
                cmd.Parameters.Add("@id_curso", SqlDbType.Int).Value = docur.IdCurso;
                cmd.Parameters.Add("@id_docente", SqlDbType.Int).Value = docur.IdDocente;
                cmd.Parameters.Add("@cargo", SqlDbType.Int).Value = docur.Cargo;
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Insert(DocenteCurso docur)
        {
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.docentes_cursos(id_curso, id_docente, cargo) " +
                                                "values(@id_curso, @id_docente, @cargo) " +
                                                "select @@identity", this.sqlConn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = docur.ID;
                cmd.Parameters.Add("@id_curso", SqlDbType.Int).Value = docur.IdCurso;
                cmd.Parameters.Add("@id_docente", SqlDbType.Int).Value = docur.IdDocente;
                cmd.Parameters.Add("@cargo", SqlDbType.Int).Value = docur.Cargo;
                docur.ID = Decimal.ToInt32((decimal)cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al crear el docente curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(DocenteCurso docur)
        {
            if (docur.State == BusinessEntity.States.Deleted)
            {
                this.Delete(docur.ID);
            }
            else if (docur.State == BusinessEntity.States.New)
            {
                this.Insert(docur);
            }
            else if (docur.State == BusinessEntity.States.Modified)
            {
                this.Update(docur);
            }
            docur.State = BusinessEntity.States.Unmodified;
        }

        public DataTable GetCursos()
        {
            DataTable cursos = new DataTable();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from cursos", sqlConn);
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

        public DataTable GetCursosDocente(int id)
        {
            DataTable cursos = new DataTable();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select * from docentes_cursos where id_docente = @id", sqlConn);
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

        public DataTable GetDocentes()
        {
            DataTable docentes = new DataTable();
            try
            {
                this.OpenConnection();
                SqlCommand cmd = new SqlCommand("select id_persona,concat( nombre,' ', apellido)apenom from personas where tipo_persona=1", sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(docentes);
            }

            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de personas", Ex);
                throw Ex;
            }
            finally
            {
                this.CloseConnection();

            }
            return docentes;
        }
    }
}