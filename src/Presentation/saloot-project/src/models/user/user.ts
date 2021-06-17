import GenderType from "@/common/Enums/genderType";

type User = {

    id: number;

    username: string;

    password: string;

    firstname: string;

    lastname: string;

    email: string;

    birthdate: string;

    phoneNumber: string;

    gender: GenderType;

    teamId: number | null;

    profilePictureId: number | null;

    roles: string[];
}
type UserCreate = Omit<User, "id" | "roles" >; // CurrentUser without id property

type UserUpdate = Omit<User, "id" | "roles" | "password">; // CurrentUser without id property

type UserList = Omit<User, "password">;

export { User, UserCreate, UserUpdate, UserList };