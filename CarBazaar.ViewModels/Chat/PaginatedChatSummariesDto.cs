using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Chat
{
    public class PaginatedChatSummariesDto
    {
        public List<ChatSummaryDto> ChatSummaries { get; set; }

        public int TotalPages { get; set; }
    }
}