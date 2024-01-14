namespace Cuentas.Options.Shared
{
    public class SqlBuilder
    {
        public string vFieldsSelect { get; set; } = "*";
        public string toSelect(string vViewName, string vWhereSelect = "")
        {
            if (vWhereSelect != "") { vWhereSelect = "WHERE " + vWhereSelect; }
            string sql = @$"SELECT {vFieldsSelect} 
                            FROM {vViewName}
                            {vWhereSelect}".Trim();

            return sql;
        }

    }
}
