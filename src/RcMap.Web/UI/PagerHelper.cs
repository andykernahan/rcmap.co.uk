// Copyright (C) 2008 Andy Kernahan
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace RcMap.Web.UI
{
    /// <summary>
    /// Helper class for rendering pagers.
    /// </summary>
    [Serializable]
    public class PagerHelper
    {
        #region Private Fields.

        private readonly int _pageSize;
        private readonly int _rowCount;
        private readonly int _currentPage;
        private readonly int _currentRow;
        private readonly int _pageCount;
        private readonly bool _isFirstPage;
        private readonly bool _isLastPage;

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the PagerHelper class.
        /// </summary>
        /// <param name="currentPage">The current page number (zero based).</param>
        /// <param name="pageSize">The number of rows per page.</param>
        /// <param name="rowCount">The total number of rows.</param>
        public PagerHelper(int currentPage, int pageSize, int rowCount) {

            _currentPage = Math.Max(currentPage, 0);
            _pageSize = Math.Max(pageSize, 1);
            _rowCount = Math.Max(rowCount, 0);            
            _currentRow = _currentPage * _pageSize;
            _pageCount = 0;
            if(_pageSize > 0) {
                _pageCount = _rowCount / _pageSize;
                if(_rowCount % _pageSize != 0)
                    ++_pageCount;
            }
            _isFirstPage = _currentPage == 0;
            _isLastPage = _currentPage >= _pageCount - 1;             
        }

        /// <summary>
        /// Gets the current page number (zero based).
        /// </summary>
        public int CurrentPage {

            get { return _currentPage; }
        }

        /// <summary>
        /// Gets the current row number (zero based).
        /// </summary>
        public int CurrentRow {

            get { return _currentRow; }
        }

        /// <summary>
        /// Gets the total number of rows.
        /// </summary>
        public int RowCount {

            get { return _rowCount; }
        }

        /// <summary>
        /// Gets the size of each page.
        /// </summary>
        public int PageSize {

            get { return _pageSize; }
        }

        /// <summary>
        /// Gets the total number of pages given the page size and total row count.
        /// </summary>
        public int PageCount {

            get { return _pageCount; }
        }

        /// <summary>
        /// Gets a value indicating if this pager is on the first page.
        /// </summary>
        public bool IsFirstPage {

            get { return _isFirstPage; }
        }

        /// <summary>
        /// Gets a value indicating if this pager is on the last page.
        /// </summary>
        public bool IsLastPage {

            get { return _isLastPage; }
        }

        #endregion
    }
}
