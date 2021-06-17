import { User } from "@/models/user/user";
import Token from "@/models/authentication/token";

type UserSignInViewModel = {

    userViewModel: User;

    token: Token;

}

export default UserSignInViewModel;