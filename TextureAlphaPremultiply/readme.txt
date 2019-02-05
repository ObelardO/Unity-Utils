Simple extension method for Texture2D
which solve problem with white edges on transparent textures loaded at runtime.

Just call <Texture2D>texture.PremultiplyAlpha(); after texture loaded.