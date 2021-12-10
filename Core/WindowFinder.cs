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
using System;

namespace Switcheroo.Core
{
    public class WindowFinder
    {

        public List<AppWindow> GetWindows()
        {
            var appWindows = new frigo::WindowFinder().Windows.ToList();
            var filtered = AppWindow.AllToplevelWindows
                .Where(a =>
                {
                    var match = appWindows.Find(h => h.handle == a.HWnd) != frigo.WindowHandle.Null;
                    return a.IsAltTabWindow() && match;
                }).ToList();

            var lines = String.Join("\n", filtered.Select(x => "title: " + x.Title));
            Log.Information("lines: {0}", lines);
            return filtered;
        }
    }
}