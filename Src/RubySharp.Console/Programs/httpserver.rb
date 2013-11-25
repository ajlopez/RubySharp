Byte = System.Byte
HttpListener = System.Net.HttpListener

FileStream = System.IO.FileStream
FileMode = System.IO.FileMode
Path = System.IO.Path
File = System.IO.File

Root = "c:/apache-tomcat-6.0.18/webapps/docs"

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

	filename = Path.Combine(Root, filename)

	puts filename

	if File.Exists(filename)
		input = FileStream.new(filename, FileMode::Open)
		bytes = System.Array.CreateInstance(Byte, 1024)
		nbytes = input.Read(bytes, 0, bytes.Length)

		while nbytes > 0
			context.Response.OutputStream.Write(bytes, 0, nbytes)
			nbytes = input.Read(bytes, 0, bytes.Length)
		end

		input.Close()
		context.Response.OutputStream.Close()
	else
		context.Response.Abort()
	end
end

listener.Start()

while true
    context = listener.GetContext()
    puts "new request"
    process(context)
end        