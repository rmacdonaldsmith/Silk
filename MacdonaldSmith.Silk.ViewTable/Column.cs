using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacdonaldSmith.Silk.ViewTable
{
    internal class Column<T> : ColumnBase
    {
        private T[] _values;
        private readonly T _defaultValue;

        public Column(T defaultValue, int rowCount, string columnName)
            : base(rowCount, columnName)
        {
            _defaultValue = defaultValue;
        }

        public override void Clear()
        {
            for (int index = 0; index < _values.Length; index++)
            {
                _values[index] = _defaultValue;
            }
        }

        public override void Clear(int usedRowCount)
        {
            for (int index = 0; index < usedRowCount; index++)
            {
                _values[index] = _defaultValue;
            }
        }

        public void Update(int rowIndex, T value)
        {
            _values[rowIndex] = value;
        }

        public override T GetValue<T>(int rowIndex)
        {
            return _values[rowIndex];
        }

        public override void ReSize(int rowCount)
        {
            Array.Resize(ref _values, rowCount);
            _rowCount = rowCount;
        }
    }
}
