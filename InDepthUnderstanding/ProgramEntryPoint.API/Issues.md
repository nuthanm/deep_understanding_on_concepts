## Issues while learning asp.net core API

### Issue 1
This localhost page can’t be found no web page was found for the web address: https://localhost:7073/swagger
HTTP ERROR 404

**Reason:**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

**Solution:** Due to the condition we got that issue
    app.UseSwagger();
    app.UseSwaggerUI();

