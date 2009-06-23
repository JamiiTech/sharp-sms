using System;
using System.Collections;

namespace Devshock.Common {
  public class ByteBuilder {
    const int DefaultInitialCapacity = 0x10;
    byte[] _items;
    int _size;
    int _version;

    public ByteBuilder() {
      _items = new byte[0x10];
    }

    public ByteBuilder(int initialCapacity) {
      if (initialCapacity < 0)
        throw new ArgumentOutOfRangeException("initialCapacity", initialCapacity,
                                              "The initial capacity can't be smaller than zero.");
      if (initialCapacity == 0)
        initialCapacity = 0x10;
      _items = new byte[initialCapacity];
    }

    public ByteBuilder(byte[] array) {
      _items = array;
      _size = array.Length;
    }

    ByteBuilder(int initialCapacity, bool forceZeroSize) {
      if (!forceZeroSize)
        throw new InvalidOperationException("Use MyArrayList(int)");
      _items = null;
    }

    ByteBuilder(byte[] array, int index, int count) {
      _items = new byte[count];
      if (count == 0)
        _items = new byte[0x10];
      else
        _items = new byte[count];
      Array.Copy(array, index, _items, 0, count);
      _size = count;
    }

    public virtual int Capacity {
      get { return _items.Length; }
      set {
        if (value < _size)
          throw new ArgumentOutOfRangeException("Capacity", value, "Must be more than count.");
        var destinationArray = new byte[value];
        Array.Copy(_items, 0, destinationArray, 0, _size);
        _items = destinationArray;
      }
    }

    public virtual int Count {
      get { return _size; }
    }

    public virtual bool IsFixedSize {
      get { return false; }
    }

    public virtual bool IsReadOnly {
      get { return false; }
    }

    public virtual bool IsSynchronized {
      get { return false; }
    }

    public virtual byte this[int index] {
      get {
        if ((index < 0) || (index >= _size))
          throw new ArgumentOutOfRangeException("index", index,
                                                "Index is less than 0 or more than or equal to the list count.");
        return _items[index];
      }
      set {
        if ((index < 0) || (index >= _size))
          throw new ArgumentOutOfRangeException("index", index,
                                                "Index is less than 0 or more than or equal to the list count.");
        _items[index] = value;
        _version++;
      }
    }

    public virtual object SyncRoot {
      get { return this; }
    }

    public virtual int Add(byte value) {
      if (_items.Length <= _size)
        EnsureCapacity(_size + 1);
      _items[_size] = value;
      _version++;
      return _size++;
    }

    public virtual void AddRange(byte[] c) {
      if (c != null)
        InsertRange(_size, c);
    }

    public virtual void AddRange(byte[] c, int count) {
      var destinationArray = new byte[count];
      Array.Copy(c, 0, destinationArray, 0, count);
      InsertRange(_size, destinationArray);
    }

    public virtual int BinarySearch(byte value) {
      int num;
      try {
        num = Array.BinarySearch(_items, 0, _size, value);
      } catch (InvalidOperationException exception) {
        throw new ArgumentException(exception.Message);
      }
      return num;
    }

    public virtual int BinarySearch(byte value, IComparer comparer) {
      int num;
      try {
        num = Array.BinarySearch(_items, 0, _size, value, comparer);
      } catch (InvalidOperationException exception) {
        throw new ArgumentException(exception.Message);
      }
      return num;
    }

    public virtual int BinarySearch(int index, int count, byte value, IComparer comparer) {
      int num;
      try {
        num = Array.BinarySearch(_items, index, count, value, comparer);
      } catch (InvalidOperationException exception) {
        throw new ArgumentException(exception.Message);
      }
      return num;
    }

    internal static void CheckRange(int index, int count, int listCount) {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index", index, "Can't be less than 0.");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", count, "Can't be less than 0.");
      if (index > (listCount - count))
        throw new ArgumentException("Index and count do not denote a valid range of elements.", "index");
    }

    public virtual void Clear() {
      Array.Clear(_items, 0, _size);
      _size = 0;
      _version++;
    }

    public virtual object Clone() {
      return new ByteBuilder(_items, 0, _size);
    }

    public virtual bool Contains(byte value) {
      return (IndexOf(value, 0, _size) > -1);
    }

    internal virtual bool Contains(byte value, int startIndex, int count) {
      return (IndexOf(value, startIndex, count) > -1);
    }

    public virtual void CopyTo(Array array) {
      Array.Copy(_items, array, _size);
    }

    public virtual void CopyTo(Array array, int index) {
      CopyTo(0, array, index, _size);
    }

    public virtual void CopyTo(int index, Array array, int arrayIndex, int count) {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new ArgumentException("Must have only 1 dimensions.", "array");
      Array.Copy(_items, index, array, arrayIndex, count);
    }

    void EnsureCapacity(int count) {
      if (count > _items.Length) {
        int num = _items.Length << 1;
        if (num == 0)
          num = 0x10;
        while (num < count)
          num = num << 1;
        var destinationArray = new byte[num];
        Array.Copy(_items, 0, destinationArray, 0, _items.Length);
        _items = destinationArray;
      }
    }

    public virtual int IndexOf(byte value) {
      return IndexOf(value, 0);
    }

    public virtual int IndexOf(byte[] value) {
      return IndexOf(value, 0);
    }

    public virtual int IndexOf(byte value, int startIndex) {
      return IndexOf(value, startIndex, _size - startIndex);
    }

    public virtual int IndexOf(byte[] value, int startIndex) {
      return IndexOf(value, startIndex, _size - startIndex);
    }

    public virtual int IndexOf(byte value, int startIndex, int count) {
      if ((startIndex < 0) || (startIndex > _size))
        throw new ArgumentOutOfRangeException("startIndex", startIndex, "Does not specify valid index.");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", count, "Can't be less than 0.");
      if (startIndex > (_size - count))
        throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
      return Array.IndexOf(_items, value, startIndex, count);
    }

    public virtual int IndexOf(byte[] value, int startIndex, int count) {
      if ((startIndex < 0) || (startIndex > _size))
        throw new ArgumentOutOfRangeException("startIndex", startIndex, "Does not specify valid index.");
      if (count < 0)
        throw new ArgumentOutOfRangeException("count", count, "Can't be less than 0.");
      if (startIndex > (_size - count))
        throw new ArgumentOutOfRangeException("count", "Start index and count do not specify a valid range.");
      int index = 0;
      for (int i = startIndex; i < count; i++)
        if (_items[i] == value[index]) {
          index++;
          if (index == value.Length)
            return ((startIndex + 1) + (i - index));
        } else
          index = 0;
      return -1;
    }

    public virtual void Insert(int index, byte value) {
      if ((index < 0) || (index > _size))
        throw new ArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
      Shift(index, 1);
      _items[index] = value;
      _size++;
      _version++;
    }

    public virtual void InsertRange(int index, byte[] c) {
      if (c != null) {
        if ((index < 0) || (index > _size))
          throw new ArgumentOutOfRangeException("index", index, "Index must be >= 0 and <= Count.");
        int length = c.Length;
        if (_items.Length < (_size + length))
          EnsureCapacity(_size + length);
        if (index < _size)
          Array.Copy(_items, index, _items, index + length, _size - index);
        if (this == c.SyncRoot) {
          Array.Copy(_items, 0, _items, index, index);
          Array.Copy(_items, (index + length), _items, (index << 1), (_size - index));
        } else
          c.CopyTo(_items, index);
        _size += c.Length;
        _version++;
      }
    }

    public virtual int LastIndexOf(byte value) {
      return LastIndexOf(value, _size - 1);
    }

    public virtual int LastIndexOf(byte value, int startIndex) {
      return LastIndexOf(value, startIndex, startIndex + 1);
    }

    public virtual int LastIndexOf(byte value, int startIndex, int count) {
      return Array.LastIndexOf(_items, value, startIndex, count);
    }

    public virtual byte ReadByte(ref int StartPosition) {
      byte num = _items[StartPosition];
      StartPosition++;
      return num;
    }

    public virtual byte[] ReadBytes(ref int StartPosition, int Length) {
      byte[] buffer = ToArray(StartPosition, Length);
      StartPosition += Length;
      return buffer;
    }

    public virtual byte[] ReadBytesUntil(ref int StartPosition, byte UntilSearch) {
      int index = IndexOf(UntilSearch, StartPosition);
      if (index < 0)
        throw new Exception("Parsing error");
      int length = index - StartPosition;
      if (length < 1) {
        StartPosition++;
        return new byte[0];
      }
      byte[] buffer = ReadBytes(ref StartPosition, length);
      StartPosition++;
      return buffer;
    }

    public virtual void Remove(byte value) {
      int index = IndexOf(value);
      if (index > -1)
        RemoveAt(index);
      _version++;
    }

    public virtual void Remove(byte[] value) {
      int index = IndexOf(value);
      if (index > -1)
        RemoveRange(index, value.Length);
      _version++;
    }

    public virtual void RemoveAt(int index) {
      if ((index < 0) || (index >= _size))
        throw new ArgumentOutOfRangeException("index", index, "Less than 0 or more than list count.");
      Shift(index, -1);
      _size--;
      _version++;
    }

    public virtual void RemoveRange(int index, int count) {
      CheckRange(index, count, _size);
      Shift(index, -count);
      _size -= count;
      _version++;
    }

    public virtual void Reverse() {
      Array.Reverse(_items, 0, _size);
      _version++;
    }

    public virtual void Reverse(int index, int count) {
      CheckRange(index, count, _size);
      Array.Reverse(_items, index, count);
      _version++;
    }

    public virtual void SetRange(int index, ICollection c) {
      if (c == null)
        throw new ArgumentNullException("c");
      if ((index < 0) || ((index + c.Count) > _size))
        throw new ArgumentOutOfRangeException("index");
      c.CopyTo(_items, index);
      _version++;
    }

    void Shift(int index, int count) {
      if (count > 0)
        if ((_size + count) > _items.Length) {
          int num = (_items.Length > 0) ? (_items.Length << 1) : 1;
          while (num < (_size + count))
            num = num << 1;
          var destinationArray = new byte[num];
          Array.Copy(_items, 0, destinationArray, 0, index);
          Array.Copy(_items, index, destinationArray, index + count, _size - index);
          _items = destinationArray;
        } else
          Array.Copy(_items, index, _items, index + count, _size - index);
      else if (count < 0) {
        int sourceIndex = index - count;
        Array.Copy(_items, sourceIndex, _items, index, _size - sourceIndex);
      }
    }

    public virtual void Sort() {
      Array.Sort(_items, 0, _size);
      _version++;
    }

    public virtual void Sort(IComparer comparer) {
      Array.Sort(_items, 0, _size, comparer);
    }

    public virtual void Sort(int index, int count, IComparer comparer) {
      CheckRange(index, count, _size);
      Array.Sort(_items, index, count, comparer);
    }

    public virtual byte[] ToArray() {
      return ToArray(0, _size);
    }

    public virtual Array ToArray(Type elementType) {
      Array array = Array.CreateInstance(elementType, _size);
      CopyTo(array);
      return array;
    }

    public virtual byte[] ToArray(int index, int count) {
      var array = new byte[count];
      CopyTo(index, array, 0, count);
      return array;
    }

    public virtual void TrimToSize() {
      if (_items.Length > _size) {
        byte[] buffer;
        if (_size == 0)
          buffer = new byte[0x10];
        else
          buffer = new byte[_size];
        Array.Copy(_items, 0, buffer, 0, _size);
        _items = buffer;
      }
    }
  }
}