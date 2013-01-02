using System;
namespace MacdonaldSmith.Silk.ViewTable
{
	public abstract class ColumnBase
	{
		protected readonly string _columnName;
		protected int _rowCount;

        public int RowCount
        {
            get { return _rowCount; }
        }

        public string ColumnName
        {
            get { return _columnName; }
        }

	    protected ColumnBase (int rowCount, string columnName)
		{
			_rowCount = rowCount;
			_columnName = columnName;
		}
		
		public abstract void Clear();
		
		public abstract void Clear(int usedRowCount);

	    public abstract void ReSize(int rowCount);

        public abstract T GetValue<T>(int rowIndex);
	}
	
    //public class StringColumn : ColumnBase
    //{
    //    private string[] _values;
		
    //    public StringColumn(int rowCount, string columnName) 
    //        : base(rowCount, columnName)
    //    {
    //        _values = new string[]{};
    //    }
		
    //    public void Update(int index, string value)
    //    {
    //        _values[index] = value;
    //    }
		
    //    public override void Clear ()
    //    {
    //        Clear(_values.Length);
    //    }
		
    //    public override void Clear (int usedRowCount)
    //    {
    //        //only need to clear up to the used row count
    //        for(int i = 0; i < usedRowCount -1; i++)
    //        {
    //            _values[i] = string.Empty;
    //        }
    //    }
    //}
}

