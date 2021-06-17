type Role = {

    id: number;

    name: string;

    description: string;

    normalizedName: string;

}

type RoleCreateOrUpdate = Omit<Role, "id">; // CurrentUser without id property

type RoleList = Role;

export { Role, RoleCreateOrUpdate, RoleList };