# Partial-File-Renamer
A C# Utility made to batch rename selections of file names.
___________________________________________________________

------------------------------------------------------------------------------------------

The current build does not support the ability to replace strings inside the file yet because
of the potential destructive nature of editing files of unknown format and encoding. While this
functionality is being worked on for future releases, the builds available currently are intended
for renaming only.

------------------------------------------------------------------------------------------
So what does the program do?

Partial-File-Renamer allows you to select a folder and batch rename files with common strings.
This means that you can rename all of them at once by taking out that string and inserting a
new one of the user's selection.

For example, with these files:

Vacation-Photo-Greece01.png<br />
Vacation-Photo-Greece02.png<br />
Vacation-Photo-Greece03.png<br />
Vacation-Photo-Italy01.png<br />
Vacation-Photo-Germany01.png<br />

We can select a common string and either replace or remove it.

rename "C:\Users\Name\Pictures\Vacation" "Vacation-Photo" "Vacation-"

We can have all of the files automatically renamed to:

Vacation-Greece01.png<br />
Vacation-Greece02.png<br />
Vacation-Greece03.png<br />
Vacation-Italy01.png<br />
Vacation-Germany01.png<br />

Alternatively, we could do:

rename "C:\Users\Name\Pictures\Vacation" "Vacation-Photo-" ""

And get:
Greece01.png<br />
Greece02.png<br />
Greece03.png<br />
Italy01.png<br />
Germany01.png<br />

------------------------------------------------------------------------------------------

The Console version also supports the ability to overwrite any files detected to have the name our files are renamed to. It can also filter out file extensions, or filter in file extensions, so that we can select which types of files are edited. The user may also select whether the program effects all subdirectories or
only the top-most folder. All of these can be selected via their own commands.

------------------------------------------------------------------------------------------
