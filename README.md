# Partial-File-Renamer
A C# Utility made to batch rename selections of file names.
___________________________________________________________

------------------------------------------------------------------------------------------

The current build does not support the ability to replace strings inside the file yet because of the potential
destructive nature of editing files of unknown format and encoding. While this functionality is being worked on
for future releases, the builds available currently are intended for renaming only.

------------------------------------------------------------------------------------------
So what does the program do?

Partial-File-Renamer allows you to select a folder and batch rename files with common strings.
This means that you can rename all of them at once by taking out that string and inserting a
new one of the user's selection.

For example, with these files:

Vacation-Photo-Greece01.png
Vacation-Photo-Greece02.png
Vacation-Photo-Greece03.png
Vacation-Photo-Italy01.png
Vacation-Photo-Germany01.png

We can select a common string and either replace or remove it.

rename "C:\Users\Name\Pictures\Vacation" "Vacation-Photo" "Vacation-"

We can have all of the files automatically renamed to:

Vacation-Greece01.png
Vacation-Greece02.png
Vacation-Greece03.png
Vacation-Italy01.png
Vacation-Germany01.png

Alternatively, we could do:

rename "C:\Users\Name\Pictures\Vacation" "Vacation-Photo-" ""

And get:
Greece01.png
Greece02.png
Greece03.png
Italy01.png
Germany01.png

------------------------------------------------------------------------------------------

The GUI version also supports the ability to overwrite any files detected to have the name our files are renamed to. It can also filter out file extensions, or filter in file extensions, so that we can select which types of files are edited. The user may also select whether the program effects all subdirectories or
only the top-most folder. All of these can be selected via their own commands.

------------------------------------------------------------------------------------------