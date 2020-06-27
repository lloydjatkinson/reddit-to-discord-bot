Everything in here was generated automatically by one of the various online JSON to C# class generators.

In order to reduce future maintenance I have left them almost exactly the same as the generator created them, this is so that I don't need to keep any changes I make in sync should the API be updated and I need to create these classes again.

Changes:
 * Make the `thumbnail_width` and `thumbnail_height` properties nullable. The Reddit API isn't very logical and in some places poorly designed.
 * Comment out Edited in Data. Not sure how to handle this with System.Text.Json - it's sometimes a boolean and sometimes a date. Genius.