import { IBaseDomain } from "../../base/IBaseDomain";
import { IQuizDTO } from "../IQuizDTO";
import { IQuizInvitationDTO } from "../IQuizInvitationDTO";
import { ISavedQuestionDTO } from "../ISavedQuestionDTO";
import { ITeamDTO } from "../ITeamDTO";
import { ITeamUserDTO } from "../ITeamUserDTO";
import { IUserFriendDTO } from "../IUserFriendDTO";
export interface IAppUserCustomDTO extends IBaseDomain {
    firstName: string;
    lastName: string;

    sentRequests: IUserFriendDTO[];
    receivedRequests: IUserFriendDTO[];
    quizInvitations: IQuizInvitationDTO[];
    teamUsers: ITeamUserDTO[];
    quizzes: IQuizDTO[];
    savedQuestions: ISavedQuestionDTO[];
    
}