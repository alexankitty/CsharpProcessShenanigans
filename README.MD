# Real name TBD
Extremely WIP

End goal: Usurp System.Diagnostics.Process to be able to request process info from a remote computer.

Why: LiveSplit has some frustrating issues under Linux (WINE). If you could run one instance and set the computer to read process data from, would alleviate the bulk of the frustration of using it.

## FAQ (no one has asked these - frequent is a misnomor)

**Q: Why not just run livesplit under WINE?**

A: I'm already doing this, but the implementation is extremely clunky. Please refer to [livesplit-linux-install](https://github.com/alexankitty/livesplit-linux-install/) for implementation details. Having to run LiveSplit under every single prefix for autosplitting to work correctly is awful. This project aims to simplify the entire implementation down to just running a memory server that the main LiveSplit can send requests to. This in turn removes the requirements of the launch script to need to install gdiplus for the timer to look correct and fix the periodic stutter, and removes the need to run a global hotkey server.

**Q: When will this be completed?**

A: How many spoons can you give me?

**Q: No one runs on Linux.**

A: Not a question, also not true.

**Q: Can I pay you to finish this?**

A: You're welcome to donate to my [ko-fi](https://ko-fi.com/alexankitty), but this will not speed up development as I do this in my free time.

**Q: Will you implement autosplitting for Linux programs?**

A: Not at this time. LiveSplit's TCP server is robust enough that an autosplitter could be made with something that actually runs natively instead of under LiveSplit.

**Q: Why are you bad at programming?**

A: I don't do it for a living.

**Q: You should do it X way.**

A: Not a question. Put in a pull request if you want to help.

**Q: Why are you sassy?**

A: Why are you not?