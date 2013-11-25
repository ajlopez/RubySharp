Array = System.Array
Byte = System.Byte
HttpListener = System.Net.HttpListener

root = "c:/apache-tomcat-6.0.18/webapps/docs"

bytes = Array.CreateInstance(Byte, 1024)

listener = HttpListener.new
listener.Prefixes.Add("http://*:8000/")

def process(context)
	filename = context.Request.Url.AbsolutePath

	if filename == ""
		filename = "index.html"
	end

	if filename == "/"
		filename = "index.html"
	end

	if filename[0] == "/"
		filename = filename.Substring(1)
	end

	puts context.Request.Url.AbsolutePath
	puts filename

	filename = Path.Combine(root, filename)

	puts filename
end

