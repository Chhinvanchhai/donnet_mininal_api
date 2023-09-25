class UserController
{
    public ResponseApi resResponeApi;
    public UserController(){
        resResponeApi = new ResponseApi();
    }
    public ResponseApi getuser()
    {
        var roles = new[]{
            new Roles("Vanchhai", "Admin"),
            new Roles("Vanchhai", "Guest"),
            new Roles("Vanchhai", "Dear"),
            new Roles("Vanchhai", "User"),
       };
        var user = new List<Person>() {
            new Person("nothing",  roles),
            new Person("nothing2",  roles)
        };
        // var dd = await db.c_minial_api.ToListAsync();
        resResponeApi.statusCode = 200;
        resResponeApi.data = user;
        return resResponeApi;
    }

}