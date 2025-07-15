import os
import shutil
from tkinter.filedialog import askdirectory

# general files
ALLOWED_FORMATS_IMAGE = ['.png', '.jpg', '.gif', '.tiff', '.raw', '.psd'] 
ALLOWED_FORMATS_DOCUMENTS = ['.txt', '.doc', '.docx', '.md', '.rtf', '.pages', '.eml', '.msg', '.odt', '.sty', '.readme','.pdf']
ALLOWED_FORMATS_SLIDES = ['.pptx', '.ppt', '.odp', '.key']
ALLOWED_FORMATS_TABLES = ['.xlsx', '.xls' ,'.csv', '.ods', '.tsv', '.dbf']

# code files 
ALLOWED_FORMATS_C = ['.c', '.h', '.cpp']
ALLOWED_FORMATS_PYTHON = ['.py', '.pyc', '.pyo', '.pyw', '.pyx', '.pyd']
ALLOWED_FROMATS_JAVA = ['.java', '.class', '.jar', '.war', '.ear', 'pom.xml', '.jsp', '.properties']
ALLOWED_FORMATS_SQL = ['.sql', '.ddl', '.dml', '.bak', '.backup', '.query', '.qry']
ALLOWED_FORMATS_WEB = ['.html', '.css', '.php', '.js']
ALLOWED_FORMATS_CODE = (
    ALLOWED_FORMATS_C +
    ALLOWED_FORMATS_PYTHON +
    ALLOWED_FROMATS_JAVA +
    ALLOWED_FORMATS_SQL +
    ALLOWED_FORMATS_WEB
)

# folders beeing created inside 'code' folder, if no code folder exists
SUBFOLDERS_FOR_CODEFOLDER = ["C", "Python", "Java", "Sql", "Website_Development"]

# __file__ == path of script 
# currentFolder = os.path.dirname(__file__) 
# currentFolder = r"C:\Users\chris\OneDrive\Desktop"
currentFolder = askdirectory(title="Select the folder to clean up")

# folders to create if they don't exist
imageFolder = os.path.join(currentFolder, "Images")
documentFolder = os.path.join(currentFolder, "Documents")
slidesFolder = os.path.join(currentFolder, "Slides")
tablesFolder = os.path.join(currentFolder, "Tables")

# folder to create if coding files are present in the directory
codeFolder = os.path.join(currentFolder, "Code")

# create folders if they don't exist
if not os.path.isdir(imageFolder):
	os.mkdir(imageFolder)

if not os.path.isdir(documentFolder):
	os.mkdir(documentFolder)

if not os.path.isdir(slidesFolder):
	os.mkdir(slidesFolder)

if not os.path.isdir(tablesFolder):
	os.mkdir(tablesFolder)

# function to move file from source folder into destination folder
def moveFile(fileName, sourceFolder, destFolder):
	# get path of the current file
	sourceFolderWithFile = os.path.join(sourceFolder, fileName)
	# get future path of file
	destFolderWithFile = os.path.join(destFolder, fileName)
	# move file from source to dest
	shutil.move(sourceFolderWithFile, destFolderWithFile)

# iterate through loose files and add them to the appropriate folder 
for file in os.listdir(currentFolder):
	if os.path.isfile(file): 
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_IMAGE):
			moveFile(file, currentFolder, imageFolder)
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_DOCUMENTS):
			moveFile(file, currentFolder, documentFolder)
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_SLIDES): 
			moveFile(file, currentFolder, slidesFolder)
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_TABLES): 
			moveFile(file, currentFolder, tablesFolder)
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_CODE): 
			# create 'Code' folder if doesn't exist
			if not os.path.isdir(codeFolder):
				os.mkdir(codeFolder)
				# create coding subfolders based on 'SUBFOLDERS_FOR_CODEFOLDER'
				for subFolder in SUBFOLDERS_FOR_CODEFOLDER: 
					subFolderPath = os.path.join(codeFolder, subFolder)
					os.mkdir(subFolderPath)
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_C):
			moveFile(file, currentFolder, os.path.join(codeFolder, "C"))
		if file.lower() != "clean-up.py" and any(file.lower().endswith(format) for format in ALLOWED_FORMATS_PYTHON):
			moveFile(file, currentFolder, os.path.join(codeFolder, "Python"))
		if any(file.lower().endswith(format) for format in ALLOWED_FROMATS_JAVA):
			moveFile(file, currentFolder, os.path.join(codeFolder, "Java"))
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_SQL):
			moveFile(file, currentFolder, os.path.join(codeFolder, "Sql"))
		if any(file.lower().endswith(format) for format in ALLOWED_FORMATS_WEB):
			moveFile(file, currentFolder, os.path.join(codeFolder, "Website_Development"))