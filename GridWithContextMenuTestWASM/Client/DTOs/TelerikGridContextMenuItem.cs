using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GridWithContextMenuTestWASM.Client.DTOs
{
    public class TelerikGridContextMenuItem
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public Func<Task> Action { get; set; }
        public bool Separator { get; set; }
        public bool Disabled { get; set; }
        public List<TelerikGridContextMenuItem> Items { get; set; }
    }
}
