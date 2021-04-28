import { IQuizDTO } from "../IQuizDTO";
import { ITeamDTO } from "../ITeamDTO";

export interface IAddTeamDTO{
    team: ITeamDTO;
    members: string[];
}