using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;
using SqlComponents;
using IrisAttributes;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using IrisModels.Models;
using System.Web;
using System.Diagnostics;
using System.Text;
using System.Web.Script.Serialization;
using CoreDomain.ViewModels;
using System.Dynamic;
using SqlComponents.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using IrisModels;
using IrisUI.GridBuilder.Attributes;
using IRIS10ClockITWPF.Models;

namespace CoreDomain
{
    public sealed class CoreService
    {
        public static string currentUser = "IrisAuth"; //default User, will break for most tables unless a user is logged in to change it.
        //private static bool developerMode = true; //this should be false if publishing for production
        private const string C_SESSION_USER_INFO_KEY = "IRIS_USER_INFO";
        private const int C_SESSION_LIFESPAN = 15 * 60;
        private const int C_SALT_VALUE_SIZE = 16;
        private readonly SQLHelper _sqlHelper = new SQLHelper();
        private static HttpClient client = new HttpClient();

        #region API methods
        public async Task<string> GetVersionInfo(string portal)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://devapi.aociris.org/");
            HttpResponseMessage response = await client.GetAsync("api/apiping" + (portal != null ? "?=" + portal : ""));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        //Propably don't need this one?
        public static DataTable ToDataTable(TypeInfo model, List<object> items)
        {
            var tb = new DataTable(model.Name);
            var instance = Activator.CreateInstance(model.GetTypeInfo());

            PropertyInfo[] props = model.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.Name != "RemoteAction" || prop.Name.StartsWith("REMOTE"))
                {
                    tb.Columns.Add(prop.Name, IsNullableType(prop.PropertyType) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                }
            }

            foreach (var item in items)
            {
                instance = JsonConvert.DeserializeObject(item.ToString(), model.GetTypeInfo());
                var values = new object[tb.Columns.Count];
                var idx = 0;
                for (var i = 0; i < props.Length; i++)
                {
                    if (props[i].Name != "RemoteAction" || props[i].Name.StartsWith("REMOTE"))
                    {
                        values[idx] = props[i].GetValue(instance, null);
                        idx++;
                    }
                }
                tb.Rows.Add(values);
            }

            return tb;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        public static TypeInfo GetModelTypeInfo(string modelName)
        {
            try
            {
                Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                return a.DefinedTypes.Where(b => b.FullName == "IrisModels.Models." + modelName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static string GetToken(int? userKey = null)
        {
            CoreService self = new CoreService();
            List<SqlWhere> wheres = new List<SqlWhere>();
            try
            {
                if (userKey.HasValue)
                {
                    wheres.Add(new SqlWhere(null, null, "APIAccess", "User_Key", userKey.Value, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
                    return self.LoadModel<APIAccessModel>(wheres, conName: "IrisAuth", tenant: 0).FirstOrDefault().Token;
                }
                else
                {
                    wheres.Add(new SqlWhere(null, null, "APIAccess", "User_Key", (int)HttpContext.Current.Session["CurrentUserKey"], null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
                    return self.LoadModel<APIAccessModel>(wheres, conName: HttpContext.Current.Session["ConString"].ToString()).FirstOrDefault().Token;
                }
            }
            catch (Exception ex)
            {
                return "Cannot Load or Find User";
            }

        }

        public static int GetTokenKey(int? userKey = null)
        {
            CoreService self = new CoreService();
            List<SqlWhere> wheres = new List<SqlWhere>();
            if (userKey.HasValue)
            {
                wheres.Add(new SqlWhere(null, null, "APIAccess", "User_Key", userKey.Value, null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
                return self.LoadModel<APIAccessModel>(wheres, conName: "IrisAuth", tenant: 0).FirstOrDefault().APIAccess_Key;
            }
            else
            {
                wheres.Add(new SqlWhere(null, null, "APIAccess", "User_Key", (int)HttpContext.Current.Session["CurrentUserKey"], null, SqlWhereComparison.SqlComparer.Equal, SqlWhereAndOrOptions.SqlWhereAndOr.And));
                return self.LoadModel<APIAccessModel>(wheres, conName: HttpContext.Current.Session["ConString"].ToString()).FirstOrDefault().APIAccess_Key;
            }
        }

        private static async Task<Uri> GetObject(string name, string token)
        {
            HttpResponseMessage response = await client.GetAsync(
                "api/" + name.Replace("Model", "") + "API?token=" + token); // + "&tenant=" + currentUser);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.RequestMessage.RequestUri;
        }

        public async Task<SqlReturnModel> UpdateObject(string name, object model, string token)
        {
            SqlReturnModel retVal = new SqlReturnModel();
            if (client == null || client.BaseAddress == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://devapi.aociris.org/");
            }
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/" + name.Replace("Model", "") + "API?token=" + token, model);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            object result = await response.Content.ReadAsAsync<object>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(result), retVal);
            return retVal;
            //return response.RequestMessage.RequestUri;
        }

        public async Task<SqlReturnModel> InsertObject(string name, object model, string token)
        {
            SqlReturnModel retVal = new SqlReturnModel();
            if (client == null || client.BaseAddress == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://devapi.aociris.org/");
            }
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/" + name.Replace("Model", "") + "API?token=" + token, model);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            object result = await response.Content.ReadAsAsync<object>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(result), retVal);
            return retVal;
            //return response.RequestMessage.RequestUri;
        }

        public async Task<SqlReturnModel> DeleteObject(string name, object id, object model, string token)
        {
            SqlReturnModel retVal = new SqlReturnModel();
            if (client == null || client.BaseAddress == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://devapi.aociris.org/");
            }
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/" + name.Replace("Model", "") + "API/" + id + "?token=" + token);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            object result = await response.Content.ReadAsAsync<object>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(result), retVal);
            return retVal;
            //return response.RequestMessage.RequestUri;
        }

        static async Task<List<object>> GetTableAsync(string path)
        {
            List<object> result = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<object>>();
            }
            return result;
        }

        public async Task<object> RemoteLogin(object model)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://devapi.aociris.org/");
            SqlReturnModel retVal = new SqlReturnModel();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/RemoteLogin", model);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            object result = await response.Content.ReadAsAsync<object>();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(result), retVal);
            return retVal;
        }

        //remote sync all
        public async Task<object> RemoteSyncAll(string token, int user)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://devapi.aociris.org/");
            HttpResponseMessage response = await client.GetAsync(
                "api/RemoteSyncAPI?token=" + token + "&userkey=" + user);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            object result = await response.Content.ReadAsAsync<object>();
            return result;
        }

        //remote sync all
        public async Task<object> RemoteSyncData(string token, int user, object model)
        {
            //This function can either receive a model or make a model
            //structure it's looking for is object type = List<IEnumerable<object>>
            client = new HttpClient();
            client.BaseAddress = new Uri("https://devapi.aociris.org/");
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/RemoteSyncAPI?token=" + token + "&userkey=" + user, model);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            object result = await response.Content.ReadAsAsync<object>();
            return result;
        }

        public static async Task<IEnumerable<object>> LoadDataFromApi(string modelName, string token)
        {
            //object model = CoreService.GetModel(modelName);
            //Type t = model.GetType();
            List<object> o = new List<object>();
            try
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://devapi.aociris.org/");
                //if(developerMode) client.BaseAddress = new Uri("https://devapi.aociris.org/");
                //else client.BaseAddress = new Uri("https://devapi.aociris.org/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                Uri url = await GetObject(modelName, token);

                o = await GetTableAsync(url.PathAndQuery);
            }
            catch (Exception e)
            {
                List<object> error = new List<object>();
                error.Add(e);
                Debug.WriteLine(e);
                //return error;
            }

            return o;
        }

        public static object GetModel(string modelName)
        {
            try
            {
                Type t = null;
                Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                t = a.GetType("IrisModels.Models." + modelName);
                if (t == null)
                {
                    t = a.GetType("IrisModels.API.IrisRemote." + modelName);
                }
                return Activator.CreateInstance(t);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        #endregion

        #region Lookup Functions
        public List<ExpandoObject> LoadLookupData(Type model)
        {
            List<ExpandoObject> o = new List<ExpandoObject>();//Activator.CreateInstance<List<dynamic>>();
            string keyField = "";
            string tableName = "";
            SqlTable[] tables = null;
            List<object> values = new List<object>();
            if (model.GetCustomAttribute<ModelDataBindingsAttribute>() != null)
            {
                keyField = model.GetCustomAttribute<ModelDataBindingsAttribute>().KeyFieldName;
                tableName = model.GetCustomAttribute<ModelDataBindingsAttribute>().TableName;
            }
            else
            {
                foreach (var p in model.GetProperties())
                {
                    if (p.GetCustomAttribute<KeyAttribute>() != null)
                        keyField = p.Name;
                }
                tableName = model.Name.Replace("Model", "");
            }
            HttpContext.Current.Session["CurrentLookupTable"] = model as Type;
            DatabaseModelBindings bindings = new DatabaseModelBindings { DatabaseName = "CountyDatabase", KeyFieldName = keyField, TableName = tableName };
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, optionalTables: tables);
            SqlDataReader dr = SQLHelper.FetchDataReader(gen, bindings.DatabaseName);
            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        ExpandoObject newObject = new ExpandoObject();
                        var expandoDict = newObject as IDictionary<string, object>;

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);
                            PropertyInfo propertyInfo = model.GetProperty(name);

                            if (dr[i] != System.DBNull.Value && propertyInfo != null)
                            {
                                object val = dr[i];
                                Type newType = val.GetType();

                                if (propertyInfo.PropertyType.UnderlyingSystemType == typeof(bool))
                                {
                                    if (newType == typeof(bool))
                                        expandoDict.Add(propertyInfo.Name, val);
                                    else if (newType == typeof(byte))
                                        expandoDict.Add(propertyInfo.Name, (byte)val == 1);
                                }
                                else if (propertyInfo.PropertyType.UnderlyingSystemType == typeof(string))
                                {
                                    expandoDict.Add(propertyInfo.Name, val.ToString().Replace("#", "\\#"));
                                }
                                else
                                {
                                    expandoDict.Add(propertyInfo.Name, val);
                                }
                            }
                        }
                        o.Add(newObject);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                }
                finally
                {
                    dr.Close();
                }

            }
            return o;
        }

        public async Task<SqlReturnModel> UpdateLookupModels(IDictionary<string, object> obj, string connectionString = "CountyDatabase")
        {
            Type t = HttpContext.Current.Session["CurrentLookupTable"] as Type;

            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocUpdate, tableName);
            gen.UpdateFromExpando(obj, t);
            return await _sqlHelper.ExecuteSql(gen, connectionString);
        }

        public async Task<SqlReturnModel> InsertLookupModels(IDictionary<string, object> obj, string connectionString = "CountyDatabase")
        {
            Type t = HttpContext.Current.Session["CurrentLookupTable"] as Type;

            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }


            if (obj[tableName + "_Key"] == null) //Temporary
                obj[tableName + "_Key"] = SQLHelper.GetUniqueKey();

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocInsert, tableName);
            //wheres?.ForEach(w => gen.AddWhereParameter(w));
            gen.InsertFromExpando(obj, t);

            return await _sqlHelper.ExecuteSql(gen, connectionString);
        }

        #endregion

        #region Report Related Functions
        public List<object> ReportDataTables(bool isFilter, string[] models, string baseTable, string prop, bool allRecords, string userID, List<SqlWhere> wheres = null)
        {
            Dictionary<string, string> typeList = new Dictionary<string, string>();
            DatabaseModelBindings bindings = new DatabaseModelBindings { DatabaseName = "User28", KeyFieldName = prop.Split('.')[0] + "_Key", TableName = prop.Split('.')[0] };
            SqlTable[] tables = null;
            List<object> values = new List<object>();
            if (HttpContext.Current.Session["TypeListDictionary"] != null)
                typeList = (Dictionary<string, string>)HttpContext.Current.Session["TypeListDictionary"];

            if (models != null)
            {
                tables = new SqlTable[models.Length]; //no basemodel
                if (models.Length > 0)
                {
                    if (baseTable != prop.Split('.')[0])
                        tables[0] = new SqlTable { JoinType = "INNER", Name = baseTable, JoiningTable = prop.Split('.')[0], JoinFieldNameA = prop.Split('.')[0] + "_Key", JoinFieldNameB = prop.Split('.')[0] + "_Key" };
                    var rem = new List<string>(models);
                    rem.Remove(prop.Split('.')[0]);
                    models = rem.ToArray();
                    int count = 0;
                    for (var i = 0; i < models.Length; i++)
                    {

                        if (tables[count] == null)
                            tables[count] = new SqlTable { JoinType = "INNER", Name = models[i], JoiningTable = baseTable, JoinFieldNameA = models[i] + "_Key", JoinFieldNameB = models[i] + "_Key" };
                        else if (count + 1 < tables.Length)
                            tables[count + 1] = new SqlTable { JoinType = "INNER", Name = models[i], JoiningTable = baseTable, JoinFieldNameA = models[i] + "_Key", JoinFieldNameB = models[i] + "_Key" };
                        count++;
                    }
                }
            }
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, optionalTables: tables);
            wheres?.ForEach(w => gen.AddWhereParameter(w));

            SqlDataReader dr = SQLHelper.FetchDataReader(gen, bindings.DatabaseName);

            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        object newObject = Activator.CreateInstance<object>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);
                            if (dr[i] != DBNull.Value && name == prop.Split('.')[1])
                            {
                                if (!typeList.ContainsKey(name))
                                    typeList.Add(name, dr.GetDataTypeName(i));
                                if (isFilter)
                                {
                                    if (dr.GetDataTypeName(i) == "tinyint")
                                    {
                                        int r;
                                        int.TryParse(dr[i].ToString(), out r);
                                        if (r == 1 && !values.Contains(true))
                                        {
                                            values.Add(true);
                                        }
                                        else if (r == 0 && !values.Contains(false))
                                        {
                                            values.Add(false);
                                        }
                                    }
                                    else
                                        values.Add(dr[i]);
                                }
                                else
                                    values.Add(dr[i]);
                            }
                        }
                    }
                    HttpContext.Current.Session["TypeListDictionary"] = typeList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                }
                finally
                {
                    dr.Close();
                }

            }
            if (allRecords)
                return values.ToList();
            return values.Take(20).ToList();
        }

        public string TelerikSqlString(string props, string baseModel, string models, string relations, string userID, string wheres = null)
        {
            Dictionary<string, Guid> modelGuids = (Dictionary<string, Guid>)HttpRuntime.Cache["ModelGuids"];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<ReportModelListViewModel> mods = (List<ReportModelListViewModel>)serializer.Deserialize(models, typeof(List<ReportModelListViewModel>));
            List<ReportRelationshipViewModel> relationships = (List<ReportRelationshipViewModel>)serializer.Deserialize(relations, typeof(List<ReportRelationshipViewModel>));
            List<SqlWhereViewModel> filters = (List<SqlWhereViewModel>)serializer.Deserialize(wheres, typeof(List<SqlWhereViewModel>));
            StringBuilder sb = new StringBuilder();
            List<StringBuilder> filterStrings = new List<StringBuilder>();
            var prop = props.Split(',');
            sb.Append("SELECT ");
            int count = 0;
            foreach (var p in prop)
            {
                if (count == 0)
                    sb.Append("(" + p + ") ");
                else
                    sb.Append(", (" + p + ")");
                count++;
            }
            sb.Append(" FROM " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(baseModel)).Key);
            if (relationships != null)
            {
                foreach (var o in relationships)
                {
                    sb.Append(" " + o.JoinType + " " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(o.ToModelGUID)).Key + " ON " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(o.ModelGUID)).Key + "." + o.Prop + " = " + modelGuids.FirstOrDefault(k => k.Value == Guid.Parse(o.ToModelGUID)).Key + "." + o.ToProp);
                }
            }
            if (wheres != null && wheres != "null")
            {
                sb.Append(" WHERE ");
                var indexedWhere = 0;
                foreach (var w in filters)
                {
                    StringBuilder temp = new StringBuilder();
                    if (indexedWhere > 0)
                    {
                        if (w.AndOr == SqlWhereAndOrOptions.SqlWhereAndOr.Or)
                        {
                            sb.Append(" OR ");
                            temp.Append(" OR ");
                        }
                        else
                        {
                            sb.Append(" AND ");
                            temp.Append(" AND ");
                        }
                    }
                    if (w.Group1 != null && w.Group1 != "")
                    {
                        sb.Append(" ( ");
                    }
                    if (w.MockValue1?.ToString() == "True")
                    {
                        w.MockValue1 = "1";
                    }
                    else if (w.MockValue1?.ToString() == "False")
                    {
                        w.MockValue1 = "0";
                    }
                    if (w.MockValue2?.ToString() == "True")
                    {
                        w.MockValue2 = "1";
                    }
                    else if (w.MockValue2?.ToString() == "False")
                    {
                        w.MockValue2 = "0";
                    }
                    switch (w.MockComparator)
                    {
                        case "CONTAINS":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'"));
                            break;
                        case "STARTS WITH":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " STARTS WITH " + "'" + w.MockValue1 + "%'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " STARTS WITH " + "'" + w.MockValue1 + "%'"));
                            break;
                        case "ENDS WITH":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " ENDS WITH " + "'%" + w.MockValue1 + "'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " ENDS WITH " + "'%" + w.MockValue1 + "'"));
                            break;
                        case "BETWEEN":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " '" + w.MockValue1 + "' AND '" + w.MockValue2 + "'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " '" + w.MockValue1 + "' AND '" + w.MockValue2 + "'"));
                            break;
                        case "LIKE":
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " LIKE " + "'%" + w.MockValue1 + "%'"));
                            break;
                        default:
                            sb.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " " + "'" + w.MockValue1 + "'");
                            filterStrings.Add(temp.Append(w.MockTableName + "." + w.MockFieldName + " " + w.MockComparator + " " + "'" + w.MockValue1 + "'"));
                            break;
                    }
                    if (w.Group2 != null && w.Group2 != "")
                    {
                        sb.Append(" ) ");
                    }
                    indexedWhere++;
                }
                HttpContext.Current.Session["ReportFilterStrings"] = filterStrings;
            }
            //Select props from base inner join model on ... = ... where ...
            return sb.ToString();
        }
        #endregion

        #region Math Functions
        public async Task<int> RecordCount<T>(List<SqlWhere> wheres = null, string db = null)
        {
            SqlReturnModel sqlReturn = new SqlReturnModel();
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();
            int openPcount = 0;
            int closePcount = 0;
            DatabaseModelBindings bindings = t.GetDatabaseBindings();
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Count, bindings.TableName, bindings.KeyFieldName, tenantkey: false, tenant: HttpContext.Current.Session["CurrentTenantKey"]);
            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }

            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            sqlReturn = await _sqlHelper.ExecuteSql(gen, connectionName: db == null ? bindings.DatabaseName : db);

            if (sqlReturn.ErrorMessage == null || sqlReturn.ErrorMessage == "")
                return sqlReturn.RecordCount;
            else
                return 0; //TODO: return the error
        }

        public async Task<int> RecordCount(Type model, List<SqlWhere> wheres = null)
        {
            SqlReturnModel sqlReturn = new SqlReturnModel();
            int openPcount = 0;
            int closePcount = 0;
            DatabaseModelBindings bindings = model.GetDatabaseBindings();
            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Count, bindings.TableName, bindings.KeyFieldName, tenantkey: false, tenant: HttpContext.Current.Session["CurrentTenantKey"]);
            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }

            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            sqlReturn = await _sqlHelper.ExecuteSql(gen, connectionName: bindings.DatabaseName);

            if (sqlReturn.ErrorMessage == null || sqlReturn.ErrorMessage == "")
                return sqlReturn.RecordCount;
            else
                return 0; //TODO: return the error
        }

        public double Aggregate<T>(string type, string field, List<SqlWhere> wheres = null, string conName = "CountyDataBase")
        {
            SqlGenerator.SqlTypes sqlType = SqlGenerator.SqlTypes.Count;
            int openPcount = 0;
            int closePcount = 0;
            switch (type)
            {
                case "Count":
                    sqlType = SqlGenerator.SqlTypes.Count;
                    break;
                case "Maximum":
                    sqlType = SqlGenerator.SqlTypes.Max;
                    break;
                case "Minimum":
                    sqlType = SqlGenerator.SqlTypes.Min;
                    break;
                case "Average":
                    sqlType = SqlGenerator.SqlTypes.Avg;
                    break;
                case "Sum":
                    sqlType = SqlGenerator.SqlTypes.Sum;
                    break;
            }
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();
            DatabaseModelBindings bindings = t.GetDatabaseBindings();
            var gen = new SqlGenerator(sqlType, bindings.TableName, tenantkey: false, tenant: HttpContext.Current.Session["CurrentTenantKey"], field: field);

            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }
            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));

            var total = SQLHelper.ExecuteSqlFloat(gen, connectionName: conName);
            return total;
        }
        #endregion

        /// <summary>
        /// Uses the SQL Generator to dynamically create a SELECT statement based on 
        /// the properties of the model referenced and returns an array of the
        /// referenced model populated from the devapi.
        /// </summary>
        public IEnumerable<T> LoadModel<T>(List<SqlWhere> wheres = null, List<SqlTable> joinTables = null, int pageSize = 0, int currentPage = 0, object tenant = null, List<string> groups = null, bool api = false, string conName = null, string database = null, List<OrderVM> orderfield = null, HttpSessionStateBase session = null, string dir = "DESC")
        {
            Type t = typeof(T);
            List<T> o = Activator.CreateInstance<List<T>>();
            int openPcount = 0;
            int closePcount = 0;
            if (HttpContext.Current != null && tenant == null)
            {
                if (session != null)
                {
                    tenant = session["CurrentTenantKey"];
                }
                else tenant = HttpContext.Current.Session["CurrentTenantKey"];
            }
            DatabaseModelBindings bindings = t.GetDatabaseBindings(session: session, DBname: conName);

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, keyFieldName: bindings.KeyFieldName, tenantkey: false, tenant: tenant, pageorderdir: dir);
            if (joinTables != null)
            {
                foreach (SqlTable st in joinTables)
                {
                    gen.AddTable(st.Name, SqlGenerator.SqlJoins.Inner, st.JoinComparator, st.JoinFieldNameA, st.JoiningTable, st.JoinFieldNameB);
                }
            }
            gen.SetupPagination(pageSize, currentPage, groups);
            gen.SelectFromModel<T>();

            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }


            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            if (orderfield != null)
            {
                foreach (OrderVM ord in orderfield)
                {
                    if (ord.FieldName != null) gen.AddOrderBy(ord.FieldName, ord.Index, ord.Direction);
                }
            }


            SqlDataReader dr = SQLHelper.FetchDataReader(gen, conName != null ? conName : bindings.DatabaseName, null, api);

            if (dr != null)
            {
                try
                {
                    while (dr.Read())
                    {
                        T newObject = Activator.CreateInstance<T>();

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);

                            PropertyInfo propertyInfo = t.GetProperty(name);


                            if (dr[i] != DBNull.Value)
                            {
                                if (propertyInfo?.CanWrite == true)
                                {
                                    object val = dr[i];
                                    Type newType = val.GetType();

                                    if (propertyInfo.PropertyType.UnderlyingSystemType == typeof(string))
                                    {
                                        propertyInfo.SetValue(newObject, val.ToString().Replace("#", "\\#")); //Required for kendo
                                    }
                                    else
                                    {
                                        if (newType.ToString().Contains("Double"))
                                        {
                                            Console.WriteLine();
                                        }
                                        propertyInfo.SetValue(newObject, val);
                                    }
                                }
                            }
                        }
                        o.Add(newObject);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Failed to Open, Exception is: {ex.Message}");
                }
                finally
                {
                    dr.Close(); //Added to prevent too many connections being open
                }
            }
            else
            {
                Console.WriteLine();
            }

            return o;
        }

        public DataSet LoadDataSet<T>(object tenant, string database = null, bool api = false)
        {

            Type t = typeof(T);
            DataSet ds = new DataSet();
            string baseTable = t.GetCustomAttribute<APIContainerAttribute>().BaseModel.Name;
            foreach (PropertyInfo p in t.GetProperties())
            {
                DataTable dt = new DataTable();
                try
                {
                    dt = LoadDataTable(p.PropertyType, tenant: tenant, api: api, database: database);
                    if (dt == null)
                    {
                        dt = new DataTable();
                    }
                }
                catch (Exception e)
                {
                    dt = new DataTable();
                }

                dt.TableName = p.PropertyType.Name;
                var frc = p.GetCustomAttribute<APIForeignRelationAttribute>();
                ds.Tables.Add(dt);


                //if(frc != null)
                //{
                //    ds.Relations.Add(ds.Tables[baseTable].Columns[frc.PrimaryTableKeyName], ds.Tables[p.PropertyType.Name].Columns[frc.ForeignKeyTableName]);
                //}
            }
            return ds;
        }

        public DataTable LoadDataTable(Type t, List<SqlWhere> wheres = null, int pageSize = 0, int currentPage = 0, object tenant = null, List<string> groups = null, bool api = false, string database = null)
        {
            int openPcount = 0;
            int closePcount = 0;
            if (HttpContext.Current != null && tenant == null)
            {
                tenant = HttpContext.Current.Session["CurrentTenantKey"];
            }

            DatabaseModelBindings bindings = t.GetDatabaseBindings();

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.Select, bindings.TableName, tenantkey: false, tenant: tenant);
            gen.SetupPagination(pageSize, currentPage, groups);
            gen.SelectFromModel(t);

            if (wheres != null)
            {
                foreach (SqlWhere w in wheres)
                {
                    if (w.Group1 != null)
                        openPcount++;
                    if (w.Group2 != null)
                        closePcount++;
                }
            }

            if (openPcount == closePcount)
                wheres?.ForEach(w => gen.AddWhereParameter(w));
            else Debug.WriteLine(openPcount + " " + closePcount);

            DataTable dt = SQLHelper.FetchDataTable(gen, bindings.DatabaseName, database, api);

            return dt;
        }

        public async Task<SqlReturnModel> SprocUpdate<T>(T model, string connectionString = "CountyDatabase")
        {
            Type t = model.GetType();

            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("VIEWMODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 9, 9);
            }
            else if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocUpdate, tableName);
            gen.UpdateFromModel(model);


            return await _sqlHelper.ExecuteSql(gen, connectionString);
        }

        public async Task<SqlReturnModel> SprocInsert<T>(T model, string connectionString = "CountyDatabase")
        {
            Type t = model.GetType();

            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("VIEWMODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 9, 9);
            }
            else if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocInsert, tableName);
            gen.InsertFromModel(model);


            return await _sqlHelper.ExecuteSql(gen, connectionString);
        }

        public async Task<SqlReturnModel> SprocSRSSpecial<T>(T model, string connectionString = "CountyDatabase", string type = "")
        {
            SqlReturnModel retModel = new SqlReturnModel();
            if (type == "")
            {
                retModel.ErrorMessage = "Could not determinw what srs process to run, type: " + type;
                return retModel;
            }

            string tableName = "SRSRequest";

            switch (type)
            {
                case "Reassign":
                    var gen = new SqlGenerator(SqlGenerator.SqlTypes.Reassign, tableName);
                    gen.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen, connectionString);
                case "Complete":
                    var gen2 = new SqlGenerator(SqlGenerator.SqlTypes.Complete, tableName);
                    gen2.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen2, connectionString);
                case "Comment":
                    var gen3 = new SqlGenerator(SqlGenerator.SqlTypes.Comment, tableName);
                    gen3.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen3, connectionString);
                case "Reinstate":
                    var gen4 = new SqlGenerator(SqlGenerator.SqlTypes.Reinstate, tableName);
                    gen4.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen4, connectionString);
                case "Archive":
                    var gen5 = new SqlGenerator(SqlGenerator.SqlTypes.Archive, tableName);
                    gen5.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen5, connectionString);
                case "New":
                    var gen6 = new SqlGenerator(SqlGenerator.SqlTypes.New, tableName);
                    gen6.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen6, connectionString);
                case "Pending":
                    var gen7 = new SqlGenerator(SqlGenerator.SqlTypes.Pending, tableName);
                    gen7.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen7, connectionString);
                case "Reviewed":
                    var gen8 = new SqlGenerator(SqlGenerator.SqlTypes.Reviewed, tableName);
                    gen8.UpdateFromModel(model);
                    return await _sqlHelper.ExecuteSql(gen8, connectionString);
            }
            return retModel;
        }

        public async Task<SqlReturnModel> SprocDelete<T>(T model, string connectionString = "CountyDatabase")
        {
            Type t = model.GetType();
            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("VIEWMODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 9, 9);
            }
            else if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocDelete, tableName);
            gen.DeleteFromModel(model);
            return await _sqlHelper.ExecuteSql(gen, connectionString);
        }

        public async Task<SqlReturnModel> SprocSync<T>(T model, string connectionString = "CountyDatabase")
        {
            Type t = model.GetType();
            string tableName = t.Name;

            if (tableName.ToUpper().EndsWith("VIEWMODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 9, 9);
            }
            else if (tableName.ToUpper().EndsWith("MODEL"))
            {
                tableName = tableName.Remove(tableName.Length - 5, 5);
            }

            var gen = new SqlGenerator(SqlGenerator.SqlTypes.SprocSync, tableName);

            foreach (var item in t.GetProperties())
            {
                if (item.Name == "SelectedTableName")
                    gen.AddField(item.Name, tableName, item.GetValue(model), ParameterDirection.Input, null, SqlDbType.VarChar);
                else
                    gen.AddField(item.Name, tableName, item.GetValue(model), ParameterDirection.Input, null, SqlDbType.Int);
                //gen.BuildSqlParameter();
                //gen.SqlVariables.Add(new SqlParameter(item.Name, item.GetValue(model)));
            }
            return await _sqlHelper.ExecuteSql(gen, connectionString);
        }

        public bool StartSession<T>(string username, string password, out string token, out DateTime expiration)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "IRISUser");
            sqlGen.AddField("*");
            sqlGen.AddWhereParameter(null, "IRISUser", "UserName", username, SqlWhereComparison.SqlComparer.Equal, null); //users compare email to login, will be unique

            DatabaseModelBindings ModelBinding = new DatabaseModelBindings();

            ModelBinding = typeof(T).GetDatabaseBindings();
            DataTable dt = SQLHelper.FetchDataTable(sqlGen, ModelBinding.DatabaseName);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HashPassword"].ToString() == CryptoHelper.ComputeHash(password, dt.Rows[0]["SALT"].ToString()))
                {
                    string sessionKey = Guid.NewGuid().ToString("N");
                    DateTime sessionExpires = DateTime.Now.AddMinutes(C_SESSION_LIFESPAN);

                    IRISUserModel user = LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.User_Key == (int)dt.Rows[0]["User_Key"]);
                    user.AuthGUID = sessionKey;
                    user.AuthDate = sessionExpires;
                    user.UpdatedByUser_Key = user.User_Key;

                    SprocUpdate(user, "IrisAuth");

                    token = sessionKey;
                    expiration = sessionExpires;

                    return true;
                }
            }

            token = null;
            expiration = DateTime.MinValue;
            return false;
        }

        public bool StartSession<T>(string username, string hashpassword, string access, out string token, out DateTime expiration)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "IRISUser");
            sqlGen.AddField("*");
            sqlGen.AddWhereParameter(null, "IRISUser", "UserName", username, SqlWhereComparison.SqlComparer.Equal, null); //users compare email to login, will be unique

            DatabaseModelBindings ModelBinding = new DatabaseModelBindings();

            ModelBinding = typeof(T).GetDatabaseBindings();
            DataTable dt = SQLHelper.FetchDataTable(sqlGen, ModelBinding.DatabaseName);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HashPassword"].ToString() == hashpassword)
                {
                    string sessionKey = Guid.NewGuid().ToString("N");
                    DateTime sessionExpires = DateTime.Now.AddMinutes(C_SESSION_LIFESPAN);

                    IRISUserModel user = LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.User_Key == (int)dt.Rows[0]["User_Key"]);
                    user.AuthGUID = sessionKey;
                    user.AuthDate = sessionExpires;
                    user.UpdatedByUser_Key = user.User_Key;

                    SprocUpdate(user, "IrisAuth");

                    token = sessionKey;
                    expiration = sessionExpires;

                    return true;
                }
            }

            token = null;
            expiration = DateTime.MinValue;
            return false;
        }

        public bool ValidateSessionKey(string sessionGuid)
        {
            SqlGenerator sqlGen = new SqlGenerator(SqlGenerator.SqlTypes.Select, "IRISUser");
            sqlGen.AddField("User_Key");
            sqlGen.AddWhereParameter(null, "IRISUser", "AuthGUID", sessionGuid, SqlWhereComparison.SqlComparer.Equal, null);

            //var ret = SQLHelper.ExecuteSql(sqlGen, "IrisAuth");
            return SQLHelper.FetchValueString(sqlGen, "IrisAuth").ToString().Length > 0;
            //return true;
        }

        public void Terminate<T>(string sessionToken)
        {
            var modelBinding = typeof(T).GetDatabaseBindings();
            IRISUserModel user = LoadModel<IRISUserModel>(conName: "IrisAuth").FirstOrDefault(u => u.AuthGUID == sessionToken);
            user.AuthDate = DateTime.UtcNow.AddYears(-1);
            SprocUpdate(user, HttpContext.Current.Session["RealTenant"].ToString());
        }

        public Dictionary<string, int> BuildAuthUserInformationModel<T>(string userKey, Dictionary<string, int> roleLookup)
        {
            // BL TODO - reimplement with new security model

            // Lookup roles for this current user

            return roleLookup;
        }

        private int GetCurrentUserKey(HttpSessionStateBase session = null)
        {
            if (session == null)
                return (int)HttpContext.Current.Session["CurrentUserKey"];
            else
                return (int)session["CurrentUserKey"];
        }

        private string GetTableNameOfModel(string modelName)
        {
            if (modelName.ToUpper().EndsWith("MODEL"))
                return modelName.Remove(modelName.Length - 5, 5);
            else
                return modelName;
        }

        public string GetModelKeyValue<T>(T model)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() == 1).GetValue(model).ToString();

        }

        public string GetModelKeyFieldName<T>(T model)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() == 1).Name;
        }

        public string GetNewKeyValue<T>(T model)
        {
            if (typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute)).Count() == 1 && p.GetCustomAttributes(typeof(IsAutoNumberAttribute)).Count() != 1) != null)
            {
                return SQLHelper.GetUniqueKey();
            }
            else
            {
                return null;
            }
        }

        public static string SqlDebug;

        public static object NewKey;

        public void SetSqlVariables(string sqlDebug, object newKey)
        {
            SqlDebug = sqlDebug;
            NewKey = newKey;
        }

        public string GetSqlDebug()
        {
            return SqlDebug;
        }

        public object GetNewKey()
        {
            return NewKey;
        }
    }

}
