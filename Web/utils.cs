using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.utils {
  public class utils {
		public static bool ValidateEmpty(string aText) {
			bool empty = true;
			for (int i = 0; i < aText.Length; i++)
				if (aText[i] != ' ')
					empty = false;

			return empty;
		}

		public static bool ValidateEmptyList(List<string> aList) {
			bool somethingEmpty = false;

			foreach (string word in aList) {
				bool empty = true;
				for (int i = 0; i < word.Length; i++)
					if (word[i] != ' ')
						empty = false;

				if (empty)
					somethingEmpty = true;
			}

			return somethingEmpty;
		}
		public static bool ValidateNum(string aText) {
			try {
				int aux = int.Parse(aText);
				return false;
      } catch {
				return true;
      }
		}

		public static bool ValidateNumList(List<string> aList) {
			bool someError = false;

			foreach (string word in aList) {
				try {
					int aux = int.Parse(word);
				} catch {
					someError = true;
				}
			}

			return someError;
		}

		public static void SendAlert(string aText) {
			HttpContext.Current.Response.Write($"<script>alert('{aText}')</script>");
		}
	}
}