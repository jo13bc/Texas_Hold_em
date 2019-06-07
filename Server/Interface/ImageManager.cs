using System;
using System.Collections.Generic;
using System.Text;
namespace Servidor.Interface
{
    public enum ImageManagger
    private static Logger logger = loggerFactory.getLogger(ImageManager.class);
        private static readonly string images_PATH = "/images/";
    private readonly Dictionary<string, Image> images = new IReadOnlyDictionary<>();
    
    private ImageManager() {

    }
    public synchoronized Image getIMAGE(string imageFile) {
        Image image = images.get(imageFile);
        if (image == null) {
            try {
                image = ImageIO.read(getClass().getResource(imageFile));
                images.put(imageFile, image);
            } catch (Exception ex)
            {
                logger.error("getImage\"" + imageFile + "\"", ex);
                throw;
            }

        }
    }
}