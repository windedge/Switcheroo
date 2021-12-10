/*
 * Switcheroo - The incremental-search task switcher for Windows.
 * http://www.switcheroo.io/
 * Copyright 2009, 2010 James Sulak
 * Copyright 2014 Regin Larsen
 *
 * Switcheroo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Switcheroo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Switcheroo.  If not, see <http://www.gnu.org/licenses/>.
 */

using Serilog;
using System.Collections.Generic;
using System.Linq;
using Switcheroo.Core;
using frigo = FrigoTab;

namespace Switcheroo.Core
{
    public class WindowFinder
    {

        public List<AppWindow> GetWindows()
        {
            var appWindows = new frigo::WindowFinder().Windows.ToList();
            // Log.Information("appWindows size: {size}", appWindows.Count());

            return AppWindow.AllToplevelWindows
                .Where(a => {
                    var match = appWindows.Find(h => h.handle == a.HWnd) != frigo.WindowHandle.Null;
                    // Log.Information("match: {0}", match);

                    return a.IsAltTabWindow() && match;
                }).ToList();
        }
    }
}