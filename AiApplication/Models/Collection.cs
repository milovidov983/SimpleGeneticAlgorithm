using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiApplication.Models {
	public class Collection<TItem> {
		public TItem[] items;

		public Collection(TItem[] items) {
			this.items = items;
		}

		private int currentIndex = 0;
		
		public (bool hasValue, TItem value) GetNext() {
			if(items.Length < currentIndex) {
				var result = (true, items[currentIndex]);
				currentIndex++;
				return result;
			} else {
				currentIndex = 0;
				return (false, default);
			}
		}
	}
}
