<img src="logo.png" alt="Switcheroo" width="48px" height="48px"> Switcheroo + mods

Switcheroo is for anyone who spends more time using a keyboard than a mouse.
Instead of alt-tabbing through a (long) list of open windows, Switcheroo allows
you to quickly switch to any window by typing in just a few characters of its title.

## Screenshots

<img src="screenshot.png" alt="Screenshot of Switcheroo in action" width="540px" height="372px">

**Light and Dark mode**

<img src="light.png" alt="Screenshot of Switcheroo in action" width="540px">
<img src="dark.png" alt="Screenshot of Switcheroo in action" width="540px">

Download
--------

Grab the binaries **[here](https://github.com/daanzu/Switcheroo/releases)**

Custom Features
-------
- **Massively faster list display, especially with large number of open windows/processes.**
- **Move all un-activated top-most ("always on top") windows to the bottom of the list.**
- **Wider list.**
- Numerical quick access - Alt+number for easy switching to any of the first 10 applications.
    - **Number hint for shortcut displayed in list, both filtered and sorted.**
- Sort list by process name or title via tray icon menu or shortcut keys in-live list
- Tray icon single click open
- List items context menu with options: close, run another instance (duplicate) or bring to front
- Home/End/PageUp/PageDown keys navigation in the list
- VIM-like navigation keys Alt+j Alt+k. Alt+Up/down is working now too for when you opened the application with alt+ shortcut pressed down.

Tips
----

- You can filter by process name (.exe file) with the "`.`" character:
    - "`text.`" filters for processes with "text" in their process name.
    - "`text.readme`" filters for processes with "text" in their process name, and "readme" in their window title.
    - "`.`" is a shortcut that filters for only processes that match the current foreground window process.
    - "`.readme`" filters for only processes that match the current foreground window process,  and "readme" in their window title.
Download (light/dark)
--------
**[Download Switcheroo here](https://github.com/jsonmartin/Switcheroo/releases)**

Usage
-----

Action                         | Shortcut        | Remarks
------------------------------ | --------------- | ----------
Activate Switcheroo            | `Alt + Space`   | This shortcut can be customized in _Options_
Activate Switcheroo            | `Alt + Tab`     | Only works if enabled under _Options_
_When Switcheroo is open_      |                 |
Switch to selected window      | `Enter`,`Alt`   | Or single mouse click on a list entry
Select next/previous           | `Tab`/`Shift + Tab`, `Alt + J`/`Alt + K` |
Selection jumps                | `Home`, `End`, `PageUp`, `PageDown`| First, Last, Page up, Page down
Switch to n-th window          | `Alt + 1..0`    | For first ten on the list. 0 for tenth.
Close selected window          | `Ctrl + W`,`Alt + X`|
Options                        | `Alt + O`       |
Toggle sort by process name    | `Alt + S`       |
Dismiss Switcheroo             | `Esc`           |
