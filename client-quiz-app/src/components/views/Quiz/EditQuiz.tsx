import { TextField } from "@material-ui/core";
import React, { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { BaseService } from "../../../services/BaseService";
import { QuestionType } from "../../../base/QuestionType";
import { AppContext } from "../../../context/AppContext";
import { IQuestionAnswerDTO } from "../../../domain/IQuestionAnswerDTO";
import { IQuizDTO } from "../../../domain/IQuizDTO";
import { IQuizTopicDTO } from "../../../domain/IQuizTopicDTO";
import { ITopicQuestionDTO } from "../../../domain/ITopicQuestionDTO";



const EditQuiz = () => {
    let { id } = useParams<{ id: string }>();
    const appContext = useContext(AppContext);
    const [quizData, setQuizData] = useState({ title: '', finished: false, start: new Date(), appUserId: appContext.data.appUserId, quizTopics: [] as IQuizTopicDTO[] } as IQuizDTO);
    const [selectedTopic, setSelectedTopic] = useState({ quizId: '', topic: '', timeLimit: 0, topicQuestions: [] as ITopicQuestionDTO[] } as IQuizTopicDTO);
    const [selectedQuestions, setSelectedQuestions] = useState([] as ITopicQuestionDTO[]);
    const [selectedQuestion, setSelectedQuestion] = useState({ question: '', text: '', points: 0, type: QuestionType.NORMAL, quizTopicId: '' } as ITopicQuestionDTO);
    const [selectedAnswers, setSelectedAnswers] = useState([] as IQuestionAnswerDTO[]);
    const [selectedAnswer, setSelectedAnswer] = useState({ answer: '', correct: false, topicQuestionId: '' } as IQuestionAnswerDTO);


    const data = async () => {
        await BaseService
            .getSingle<IQuizDTO>('quiz/' + id)
            .then(data => {
                setQuizData(data!);
                console.log(data)
            });
    }

    useEffect(() => {
        data();
        console.log(quizData)
    }, []);


    function selectTopic(topic: IQuizTopicDTO) {
        setSelectedTopic(topic);
        if (topic.topicQuestions) {
            console.log(topic)
            setSelectedQuestions(topic.topicQuestions)
        }

    };

    function editSelectedTopic(e: React.ChangeEvent<HTMLInputElement>) {
        var { name, value } = e.target;
        if (isNaN(+value)) {
            setSelectedTopic({ ...selectedTopic, [name]: value });
        } else {
            setSelectedTopic({ ...selectedTopic, [name]: Number(value) });
        }
    };

    const editTopic = async () => {
        var topicData = {id: selectedTopic.id, topic: selectedTopic.topic, timeLimit: selectedTopic.timeLimit, quizId: selectedTopic.quizId } as IQuizTopicDTO
        await BaseService.updateEntity<IQuizTopicDTO>(topicData, 'quizTopics', appContext.data!.token!)
            .then(e => {
                setSelectedTopic({...selectedTopic, id: ''})
                data();
            })
    };

    const createTopic = async () => {
        var topicData = { topic: "New topic", timeLimit: 0, quizId: quizData.id } as IQuizTopicDTO
        await BaseService.createEntity<IQuizTopicDTO>(topicData, 'quizTopics', appContext.data!.token!)
            .then(e => {
                setSelectedTopic(e!)
                data();
            })
    };

    function selectQuestion(question: ITopicQuestionDTO) {

        setSelectedQuestion(question);
        console.log(question)
        if (question.questionAnswers) {
            setSelectedAnswers(question.questionAnswers)
            setSelectedAnswer({ ...selectedAnswer, topicQuestionId: question.id })
        }
    };

    function editSelectedQuestion(e: React.ChangeEvent<HTMLInputElement>) {
        var { name, value } = e.target;
        if (isNaN(+value)) {
            setSelectedQuestion({ ...selectedQuestion, [name]: value });
        } else {
            setSelectedQuestion({ ...selectedQuestion, [name]: Number(value) });
        }
    }


    const editQuestion = async () => {
        var questionData = {
            id: selectedQuestion.id, quizTopicId: selectedQuestion.quizTopicId,
            question: selectedQuestion.question, text: selectedQuestion.text, points: selectedQuestion.points, type: QuestionType.NORMAL
        } as ITopicQuestionDTO;

        var newQuestions = selectedQuestions;
        await BaseService.updateEntity<ITopicQuestionDTO>(questionData, 'topicQuestions')
            .then(e => {
                const index = newQuestions.findIndex(q => q.id === questionData.id);
                questionData.questionAnswers = selectedAnswers;
                newQuestions[index] = questionData;
                setSelectedQuestions(newQuestions);
                BaseService.createEntity<IQuestionAnswerDTO[]>(selectedAnswers, 'questionAnswers/addList')
                    .then(e => {
                    })
            })
        setSelectedQuestion({ ...selectedQuestion, id: '' })

    };


    const createQuestion = async () => {
        var topicData = { id: selectedTopic.id, topic: selectedTopic.topic, timeLimit: selectedTopic.timeLimit, quizId: selectedTopic.quizId } as IQuizTopicDTO
        await BaseService.updateEntity<IQuizTopicDTO>(topicData, 'quizTopics', appContext.data!.token!)
            .then(e => { })

        var questionData = { quizTopicId: selectedTopic.id, question: "What is love?", text: "Informative text about question", points: 0, type: QuestionType.NORMAL } as ITopicQuestionDTO
        await BaseService.createEntity<ITopicQuestionDTO>(questionData, 'topicQuestions', appContext.data!.token!)
            .then(d => {
                data()
                setSelectedQuestions([...selectedQuestions, d!])
            });
    };

    function editAnswer(e: React.ChangeEvent<HTMLInputElement>, checkBox: boolean) {
        var { name, value } = e.target;
        if (checkBox) {
            setSelectedAnswer({ ...selectedAnswer, [name]: e.target.checked });
        } else {
            setSelectedAnswer({ ...selectedAnswer, [name]: value });
        }
    }

    function addAnswer() {
        setSelectedAnswers([...selectedAnswers, selectedAnswer])
        console.log(selectedAnswers)
    }
    function correctAnswer(correct: boolean) {
        if (correct) {
            return <>Correct answer</>
        }
        return <>False answer</>
    }

    function checkSelectedQuestion(question: ITopicQuestionDTO) {
        if (selectedQuestion.id === question.id) {
            return (
                <>
                    <p></p>
                    <div className="container border rounded">
                        <p></p>
                        <form className="form-group">
                            <div className="mb-3">
                                <label className="form-label">Question text</label>
                                <input value={selectedQuestion.text} type="text" name="text" className="form-control" id="formTopic" onChange={e => editSelectedQuestion(e)} />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Question</label>
                                <input value={selectedQuestion.question} type="text" name="question" className="form-control" id="formTopic" onChange={e => editSelectedQuestion(e)} />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Max points:</label>
                                <input value={selectedQuestion.points} type="number" name="points" className="form-control" id="formTimeLimit" onChange={e => editSelectedQuestion(e)} />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Answers:</label>
                            </div>
                            {selectedAnswers.map((answer, i) => (
                                <div>{answer.answer}({correctAnswer(answer.correct)})</div>
                            ))}
                            <input value={selectedAnswer.answer} type="text" name="answer" className="form-control" id="formTopic" onChange={e => editAnswer(e, false)} />
                            <div className="form-check">
                                <input name="correct" type="checkbox" className="form-check-input" id="exampleCheck1" onChange={e => editAnswer(e, true)} />
                                <label className="form-check-label" htmlFor="exampleCheck1">Correct answer?</label>
                            </div>
                            <button type="button" className="btn btn-success d-block" onClick={() => addAnswer()}>Add a answer</button>
                            <p></p>
                            <button type="button" className="btn btn-primary" onClick={editQuestion}>Save question</button>
                            <p></p>
                        </form>
                    </div>
                    <p></p>
                </>
            )
        }
        return <><p></p></>
    }

    function checkSelectedTopic() {
        if (!selectedTopic.id) {
            return <></>
        }
        else {
            return (
                <div className="container border border-3 rounded">
                    <p></p>
                    <form className="form-group">
                        <div className="mb-3">
                            <label className="form-label">Topic name</label>
                            <input value={selectedTopic.topic} type="text" name="topic" className="form-control" id="formTopic" onChange={e => editSelectedTopic(e)} />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Time limit in seconds:</label>
                            <input value={selectedTopic.timeLimit} type="number" name="timeLimit" className="form-control" id="formTimeLimit" onChange={e => editSelectedTopic(e)} />
                        </div>
                        <div>
                            <h4>Questions:</h4>
                        </div>
                        {selectedQuestions.map((question, i) => (
                            <>
                                <div className="container border rounded">
                                    <h5>Question #{i + 1}</h5>
                                    <div>{question.text}</div>
                                    <h6 className="fw-bold">{question.question}({question.points}p)</h6>
                                    <h6 className="fw-bold">Answers:</h6>
                                    {question.questionAnswers?.map((answer, index) => (
                                        <div className="container">{answer.answer}: ({correctAnswer(answer.correct)})</div>
                                    ))}
                                    <p></p>
                                    <div className="modal-footer">
                                        <button type="button" className="btn btn-success" onClick={e => selectQuestion(question)}>Edit</button>
                                        {checkSelectedQuestion(question)}
                                    </div>


                                </div>
                                <p></p>
                            </>
                        ))}
                        <button type="button" className="btn btn-success" onClick={createQuestion}>Add a question</button>
                        <div className="modal-footer">
                            
                            <button type="button" className="btn btn-primary" onClick={editTopic}>Save topic</button>
                        </div>
                    </form>
                    <p></p>
                </div>
            )
        }
    };

    function navTopicButton(topic: IQuizTopicDTO){
        if (topic.id === selectedTopic.id) {
            return(<button type="button" className="btn btn-primary" onClick={e => selectTopic(topic)}>{topic.topic}</button>)
        }
        return(<button type="button" className="btn btn-outline-primary" onClick={e => selectTopic(topic)}>{topic.topic}</button>)
    }

    return (
        <div className="container">
            <h3>Edit {quizData.title}</h3>
            <div className="btn-group" role="group" aria-label="Basic outlined example">
                {quizData.quizTopics!.map((topic, i) => (
                    <>{navTopicButton(topic)}</>
                ))}
                <button type="button" className="btn btn-success" onClick={createTopic}>Add a question block</button>
            </div>
            <p></p>
            {checkSelectedTopic()}

        </div>

    );
    


}

export default EditQuiz;
