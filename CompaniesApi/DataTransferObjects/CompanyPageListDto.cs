using System;
using System.Collections.Generic;
using System.Text;

namespace CompaniesApi.DataTransferObjects
{
    public class CompanyPageListDto
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<CompanyListDto> Data { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        /// <value>
        /// The current page number.
        /// </value>
        public int CurrentPageNumber { get; set; }

        /// <summary>
        /// Gets or sets the data count.
        /// </summary>
        /// <value>
        /// The data count.
        /// </value>
        public int DataCount { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance has next.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this data has next; otherwise, <c>false</c>.
        /// </value>
        public bool HasNext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has previous.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this data has previous; otherwise, <c>false</c>.
        /// </value>
        public bool HasPrevious { get; set; }
    }
}
