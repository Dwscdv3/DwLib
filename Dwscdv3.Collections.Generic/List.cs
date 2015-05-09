using System;
using System.Collections;
using System.Collections.Generic;

namespace Dwscdv3.Collections.Generic {
	public class List<T> : IEnumerable<T>, IEnumerator<T> {
		int current = -1;
		T[] val;
		public int Count { get; internal set; }
		public int Capacity { get { return val.Length; } }
		public List() {
			val = new T[1];
			Count = 0;
		}
		public void Add(T value) {
			if (++Count > Capacity) {
				Array.Resize<T>(ref val, val.Length * 2);
			} val[Count - 1] = value;
		}
		public void Insert(T value, int index) {
			if (++Count > Capacity) {
				Array.Resize<T>(ref val, val.Length * 2);
			} MoveRight(index);
			val[index] = value;
		}
		public void Remove(T value) {
			for (int i = 0; i < Count; i++) {
				if (val[i].Equals(value)) {
					MoveLeft(i);
					--Count;
				}
			}
		}
		public void RemoveAt(int index) {
			MoveLeft(index);
			--Count;
		}
		void MoveLeft(int index) {
			for (int i = index; i < Count - 1; i++) {
				val[i] = val[i + 1];
			}
		}
		void MoveRight(int index) {
			for (int i = Count - 2; i > index; i--) {
				val[i] = val[i - 1];
			}
		}
		#region IEnumerator<T> 成员

		T IEnumerator<T>.Current {
			get { return val[current]; }
		}

		#endregion
		#region IDisposable 成员

		void IDisposable.Dispose() {
			
		}

		#endregion
		#region IEnumerator 成员

		object IEnumerator.Current {
			get { return val[current]; }
		}

		bool IEnumerator.MoveNext() {
			return ++current >= Count ? false : true;
		}

		void IEnumerator.Reset() {
			current = -1;
		}

		#endregion
		#region IEnumerable<T> 成员

		public IEnumerator<T> GetEnumerator() {
			return this;
		}

		#endregion
		#region IEnumerable 成员

		IEnumerator IEnumerable.GetEnumerator() {
			return this;
		}

		#endregion
	}
}