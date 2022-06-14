using Libs;
using Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
  
namespace Repositories
{
    public class CarInfoRepository : ClassForDBInstance, ICarInfoRepository
    {
        private SqlConnection Conn;

        public CarInfoRepository()
        {
            Conn = (SqlConnection)configurationMgr.Connection;
        }

        int ICarInfoRepository.Add(CarInfoModel model, IDbConnection connection)
        {
            try
            {
                Conn.Open();

                string Sql = "insert into TB_CAR_INFO(carName, carYear, carPrice, carDoor) "
                                + "values( @carName, @carYear, @carPrice, @carDoor )";
                var Comm = new SqlCommand(Sql, Conn);
                Comm.Parameters.Add("@carName", SqlDbType.NVarChar, 30);
                Comm.Parameters.Add("@carYear", SqlDbType.VarChar, 4);
                Comm.Parameters.Add("@carPrice", SqlDbType.Int);
                Comm.Parameters.Add("@carDoor", SqlDbType.Int);

                Comm.Parameters["@carName"].Value = model.carName;
                Comm.Parameters["@carYear"].Value = model.carYear;
                Comm.Parameters["@carPrice"].Value = Convert.ToInt32(model.carPrice);
                Comm.Parameters["@carDoor"].Value = Convert.ToInt32(model.carDoor);

                /*string Sql = "insert into TB_CAR_INFO(carName, carYear, carPrice, carDoor) values('";
                Sql += this.txtName.Text + "'," + this.txtYear.Text + "," +
                    Convert.ToInt32(this.txtPrice.Text) + "," + Convert.ToInt32(this.txtDoor.Text) + ")";
                var Comm = new SqlCommand(Sql, Conn);*/

                int i = Comm.ExecuteNonQuery();
                Conn.Close();

                return i;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        int ICarInfoRepository.Delete(int carNo, IDbConnection connection)
        {
            try
            {
                Conn.Open();

                string Sql = "delete from TB_CAR_INFO "
                    + "where id = @id ";

                var Comm = new SqlCommand(Sql, Conn);

                Comm.Parameters.Add("@id", SqlDbType.Int);

                Comm.Parameters["@id"].Value = carNo;

                /*string Sql = "delete from TB_CAR_INFO where id = " + Convert.ToInt32(this.lvList.SelectedItems[0].SubItems[0].Text) + "";
                var Comm = new SqlCommand(Sql, Conn);*/

                int i = Comm.ExecuteNonQuery();

                Conn.Close();

                return i;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        ArrayList ICarInfoRepository.GetAllCarInfo(IDbConnection connection)
        {
            try
            {
                Conn.Open();

                var Comm = new SqlCommand("Select * From TB_CAR_INFO", Conn);
                var myRead = Comm.ExecuteReader(CommandBehavior.CloseConnection);

                ArrayList list = new ArrayList();

                while(myRead.Read())
                {
                    list.Add(GetCarInfoModel(myRead).ToStringList());
                }
                myRead.Close();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CarInfoModel GetCarInfoModel(SqlDataReader myRead)
        {
            CarInfoModel model = new CarInfoModel();

            model.id = myRead["id"].ToString();
            model.carName = myRead["carName"].ToString();
            model.carYear = myRead["carYear"].ToString();
            model.carPrice = myRead["carPrice"].ToString();
            model.carDoor = myRead["carDoor"].ToString();

            return model;
        }


        ArrayList ICarInfoRepository.GetCarInfoByCondition(CarInfoModel model, IDbConnection connection)
        {
            try
            {
                Conn.Open();

                string Sql = "Select * From TB_CAR_INFO "
                            + "where carName = @carName or carYear = @carYear "
                            + "or carPrice = @carPrice or carDoor = @carDoor ";

                var Comm = new SqlCommand(Sql, Conn);

                Comm.Parameters.Add("@carName", SqlDbType.NVarChar, 30);
                Comm.Parameters.Add("@carYear", SqlDbType.VarChar, 4);
                Comm.Parameters.Add("@carPrice", SqlDbType.Int);
                Comm.Parameters.Add("@carDoor", SqlDbType.Int);

                Comm.Parameters["@carName"].Value = model.carName;
                Comm.Parameters["@carYear"].Value = model.carYear;
                Comm.Parameters["@carPrice"].Value =
                    Convert.ToInt32((model.carPrice == "") ? 0 : Convert.ToInt32(model.carPrice));
                Comm.Parameters["@carDoor"].Value =
                    Convert.ToInt32((model.carDoor == "") ? 0 : Convert.ToInt32(model.carDoor));


                /*var Comm = new SqlCommand("Select * From TB_CAR_INFO where carName = '" + this.txtName.Text + 
                    "' or carYear = '" + this.txtYear.Text + 
                    "' or carPrice = "
                    + Convert.ToInt32((this.txtPrice.Text == "") ? 0 : Convert.ToInt32(this.txtPrice.Text)) + 
                    " or carDoor = "
                    + Convert.ToInt32((this.txtDoor.Text == "") ? 0 : Convert.ToInt32(this.txtDoor.Text)), Conn);*/

                //var myRead = Comm.ExecuteReader();
                var myRead = Comm.ExecuteReader(CommandBehavior.CloseConnection);

                ArrayList list = new ArrayList();

                while (myRead.Read())
                {
                    list.Add(GetCarInfoModel(myRead).ToStringList());
                }
                myRead.Close();

                return list;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        int ICarInfoRepository.Update(CarInfoModel model, IDbConnection connection)
        {
            try
            {
                Conn.Open();

                string Sql = "update TB_CAR_INFO "
                        + "set carName = @carName, carYear = @carYear, "
                        + "carPrice = @carPrice, carDoor = @carDoor "
                        + "where id = @id ";

                var Comm = new SqlCommand(Sql, Conn);
                Comm.Parameters.Add("@id", SqlDbType.Int);
                Comm.Parameters.Add("@carName", SqlDbType.NVarChar, 30);
                Comm.Parameters.Add("@carYear", SqlDbType.VarChar, 4);
                Comm.Parameters.Add("@carPrice", SqlDbType.Int);
                Comm.Parameters.Add("@carDoor", SqlDbType.Int);

                Comm.Parameters["@id"].Value = Convert.ToInt32(model.id);
                Comm.Parameters["@carName"].Value = model.carName;
                Comm.Parameters["@carYear"].Value = model.carYear;
                Comm.Parameters["@carPrice"].Value = Convert.ToInt32(model.carPrice);
                Comm.Parameters["@carDoor"].Value = Convert.ToInt32(model.carDoor);

                int i = Comm.ExecuteNonQuery();

                Conn.Close();

                return i;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
