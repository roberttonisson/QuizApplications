import { IBaseDomain } from "../../base/IBaseDomain";
export interface IJwtResponseDTO {
    appUserId: string;
    token: string;
    status: string;
    firstName: string;
    lastName: string;
}