import { FC, useEffect, useState } from 'react'
import {
	FieldValues,
	SubmitHandler,
	useForm
} from 'react-hook-form';

import { toast } from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';
import { UserInfo } from '../../services/auth/authModels';
import { Modal } from './Modal';
import { Button } from '../Button';
import { Input } from '../Input';
import { Select } from '../Select';
import { useGetAllUsersQuery } from '../../services/auth/authApi';
import { useCreateConversationMutation } from '../../services/conversation/conversationApi';
import { CreateConversationCommand } from '../../services/conversation/conversationModels';
import { useAppSelector, useSessionUser } from '../../hooks';

type GroupChatModalProps = {
	isOpen?: boolean;
	onClose: () => void;
}

type Option = {
	value: string;
	label: string;
}

export const GroupChatModal: FC<GroupChatModalProps> = ({
	isOpen,
	onClose,
}) => {
	const { data, isFetching: isFetchingUsers, isLoading: isLoadingUsers } = useGetAllUsersQuery();
	const [createConversation, result] = useCreateConversationMutation();
	const [options, setOptions] = useState<Option[]>([]);
	const user = useSessionUser();
	
	useEffect(() => {
		const newOptions = data?.users.map((user) => ({
			value: user.id,
			label: user.name
		}));

		setOptions(newOptions ? [...newOptions] : []);
	}, [data])


	let navigate = useNavigate();
	const [isLoading, setIsLoading] = useState(false);

	const {
		register,
		handleSubmit,
		setValue,
		watch,
		formState: {
			errors,
		}
	} = useForm<FieldValues>({
		defaultValues: {
			name: '',
			members: []
		}
	});

	const members = watch('members');

	const onSubmit: SubmitHandler<FieldValues> = async (data) => {
		setIsLoading(true);
		const { name, members } = data;

		await createConversation({name, isGroup: true, userIds: [...members.map((x: Option) => x.value), user?.id]} as CreateConversationCommand);

		console.log("result", result);
		// try	{
		// 	await createConversation(...data)
		// }

		///to implement
		// axios.post('/api/conversations', {
		// 	...data,
		// 	isGroup: true
		// })
		// 	.then(() => {
		// 		navigate(-1);
		// 		onClose();
		// 	})
		// 	.catch(() => toast.error('Something went wrong!'))
		// 	.finally(() => setIsLoading(false));
	}

	return (
		<Modal isOpen={isOpen} onClose={onClose}>
			<form onSubmit={handleSubmit(onSubmit)}>
				<div className="space-y-12">
					<div className="border-b border-gray-900/10 pb-12">
						<h2
							className="
                text-base 
                font-semibold 
                leading-7 
                text-gray-900
              "
						>
							Create a group chat
						</h2>
						<p className="mt-1 text-sm leading-6 text-gray-600">
							Create a chat with more than 2 people.
						</p>
						<div className="mt-10 flex flex-col gap-y-8">
							<Input
								disabled={isLoading}
								label="Name"
								id="name"
								errors={errors}
								required
								register={register}
							/>
							<Select
								disabled={isLoadingUsers}
								label="Members"
								options={options}
								onChange={(value) => setValue('members', value, {
									shouldValidate: true
								})}
								value={members}
							/>
						</div>
					</div>
				</div>
				<div className="mt-6 flex items-center justify-end gap-x-6">
					<Button
						disabled={isLoading}
						onClick={onClose}
						type="button"
						secondary
					>
						Cancel
					</Button>
					<Button disabled={isLoading} type="submit">
						Create
					</Button>
				</div>
			</form>
		</Modal>
	)
}