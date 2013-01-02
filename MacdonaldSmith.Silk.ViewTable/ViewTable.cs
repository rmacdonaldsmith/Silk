using System;
using System.Collections.Generic;
using System.Linq;

namespace MacdonaldSmith.Silk.ViewTable
{
	public class ViewTable : IViewTable
	{
		private int _rowCount = 0;
		private int _usedRowCount = 0;
		private byte[] _bitMaskColumn;
		private string[] _columnNames;
	    private List<ColumnBase> _columns = new List<ColumnBase>();

        private Dictionary<int, string> _columnIndexToNameMap = new Dictionary<int, string>();
         

        //column value arrays
        private List<string[]> _stringValues = new List<string[]>(); //not primitive
        private List<Int16[]> _int16Values = new List<short[]>();
        private List<Int32[]> _int32Values = new List<int[]>();
        private List<Int64[]> _int64Values = new List<long[]>(); 
        private List<Double[]> _doubleValues = new List<double[]>();
        private List<float[]> _floatValues = new List<float[]>();
        private List<Decimal[]> _decimalValues = new List<decimal[]>(); //not primitive
        private List<DateTime[]> _dateTimeValues = new List<DateTime[]>(); 
        private List<bool[]> _boolValues = new List<bool[]>();


		public ViewTable(int rowCount)
		{
			_rowCount = rowCount;
			_bitMaskColumn = new byte[_rowCount];
			//_columnNames = new string[_rowCount];
		}
		
		public void Clear ()
		{
            _columns.ForEach(column => column.Clear());
		}

	    public void AddColumn<T>(string columnName)
	    {
	        AddColumn(columnName, default(T));
	    }

	    public void AddColumn<T>(string columnName, T defaultValue)
	    {
            if (_columns.Any(column => column.ColumnName == columnName))
            {
                throw new ArgumentException(string.Format("A column name '{0}' already exists.", columnName));
            }

            var col = new Column<T>(defaultValue, _rowCount, columnName);
            _columns.Add(col);
	    }

	    public void DeleteColumn(string columnName)
	    {
	        throw new NotImplementedException();
	    }

	    public void ReSize(int rowCount)
	    {
	        throw new NotImplementedException();
	    }

	    public void Update<T>(int rowIndex, int columnIndex, T value)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            //update the bit mask column to indicate that this row is dirty
	        _bitMaskColumn[rowIndex] = 1;

	        throw new NotImplementedException();
	    }

	    public void Update<T>(int rowIndex, string columnName, T value)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnName);

            //update the bit mask column to indicate that this row is dirty
            _bitMaskColumn[rowIndex] = 1;

	        throw new NotImplementedException();
	    }

	    public T GetValue<T>(int rowIndex, string columnName)
	    {
            CheckRowIndex(rowIndex);

	        CheckColumnIndex(columnName);

	        return _columns[1].GetValue<T>(rowIndex);
	    }

	    public T GetValue<T>(int rowIndex, int columnIndex)
	    {
            CheckRowIndex(rowIndex);

            CheckColumnIndex(columnIndex);

            return _columns[1].GetValue<T>(rowIndex);
	    }

	    public void Commit()
	    {
	        throw new NotImplementedException();
	    }

	    public void DropChanges()
	    {
	        throw new NotImplementedException();
	    }

	    public int RowCount 
        {
	        get { return _rowCount; }
	    }
	    
        public int ColumnCount 
        {
            get { return _columns.Count; }
        }

	    private void CheckRowIndex(int rowIndex)
	    {
            if (rowIndex > _rowCount)
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "You are attempting to use an index of {0} when there are only {1} rows in the table.", rowIndex,
                        _rowCount));
            }
	    }

	    private void CheckColumnIndex(string columnName)
	    {
	        if (_columns.Exists(column => column.ColumnName == columnName))
	        {
                throw new ArgumentException(string.Format("No column exists with the name '{0}'", columnName));
	        }
	    }

	    private void CheckColumnIndex(int columnIndex)
	    {
            if (columnIndex > _columns.Count)
            {
                throw new IndexOutOfRangeException(
                    string.Format(
                        "You are attempting to use an index of {0} when there are only {1} columns in the table.",
                        columnIndex, _columns.Count));
            }
	    }
	}
}

