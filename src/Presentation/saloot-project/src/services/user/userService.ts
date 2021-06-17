import httpService from "@/services/api/httpService";
import API_URLS from "@/services/api/apiUrls";
import { User, UserCreate, UserUpdate, UserList } from "@/models/user/user";
import { isEmptyObject } from "@/common/objectUtils";

class UserService {

    public async getAllUsers(): Promise<UserList[]> {
        const response = await httpService.get<UserList[]>(API_URLS.USERS);
        return response.data.data;
    }

    public async getUserById(id: number): Promise<User> {
        const response = await httpService.get<User>(`${API_URLS.USERS}/${id.toString()}`);
        return response.data.data;
    }

    public async createUser(userCreateUpdate: UserCreate): Promise<User> {
        const response = await httpService.post<User>(API_URLS.USERS, userCreateUpdate);
        return response.data.data;
    }

    public async updateUser(id: number, userUpdate: UserUpdate, profilePicture: any): Promise<User> {
        if (!isEmptyObject(profilePicture)) {
            const fileResponse = await httpService.postFile(API_URLS.FILES, profilePicture);
            userUpdate.profilePictureId = fileResponse.data.data.id;
        }

        await httpService.put<User>(`${API_URLS.USERS}/${id.toString()}`, userUpdate);

        return {

            id: id,

            username: userUpdate.username,

            firstname: userUpdate.firstname,

            lastname: userUpdate.lastname,

            email: userUpdate.email,

            birthdate: userUpdate.birthdate,

            phoneNumber: userUpdate.phoneNumber,

            gender: userUpdate.gender,

        } as User;
    }

    public async deleteUser(id: number): Promise<void> {
        await httpService.delete(`${API_URLS.USERS}/${id.toString()}`);
    }
}

export default new UserService();