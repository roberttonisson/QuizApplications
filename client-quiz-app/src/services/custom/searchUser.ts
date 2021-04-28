import { BaseService } from "../BaseService";

export async function searchUsersRequest<T>(search: string, jwt: string): Promise<T[] | null> {
    try {
        const response = await BaseService.axios
            .post("userFriends/search", { search: search }, {
                headers: {
                    Authorization: "Bearer " + jwt
                }
            })

        if (response.status >= 200 && response.status < 300) {
            return response.data;
        }

        return null;
    }
    catch (reason) {
        return reason.status;
    }
}