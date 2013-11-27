require 'Aforge'
require 'Aforge.Imaging'
require 'Aforge.Math'

bitmap = System.Drawing.Bitmap.FromFile('coins.jpg')

puts bitmap.Height
puts bitmap.Width

bitmapData = bitmap.LockBits(System.Drawing.Rectangle.new( 0, 0, bitmap.Width, bitmap.Height ), System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat )

blobCounter = AForge.Imaging.BlobCounter.new( )

blobCounter.FilterBlobs = true
blobCounter.MinHeight = 5
blobCounter.MinWidth = 5

blobCounter.ProcessImage( bitmapData )

blobs = blobCounter.GetObjectsInformation( )
bitmap.UnlockBits( bitmapData )

puts "blobs"
puts blobs.Length

blob = blobs[0]

edgePoints = blobCounter.GetBlobsEdgePoints( blob )

shapechecker = AForge.Math.Geometry.SimpleShapeChecker.new()

center = AForge.Point.new
radius = System.Single.new()

puts "IsCircle", shapechecker.IsCircle(edgePoints, center, radius)
