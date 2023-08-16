// import axios from "axios";
import { FC, useCallback, useState } from "react";

import { Avatar } from "./Avatar";
import { LoadingModal } from "./modals/LoadingModal";
import { useNavigate } from "react-router-dom";
import { UserInfo } from "../services/auth/authModels";

type UserBoxProps = {
  data: UserInfo
}

export const UserBox: FC<UserBoxProps> = ({ 
  data
}) => {
  const navigation = useNavigate();
  const [isLoading, setIsLoading] = useState(false);

  const handleClick = useCallback(() => {
    setIsLoading(true);
    console.log("not implermeted yet");
    setIsLoading(false);

    // axios.post('/api/conversations', { userId: data.id })
    // .then((data) => {
    //   router.push(`/conversations/${data.data.id}`);
    // })
    // .finally(() => setIsLoading(false));
  }, [data, navigation]);

  return (
    <>
      {isLoading && (
        <LoadingModal />
      )}
      <div
        onClick={handleClick}
        className="
          w-full 
          relative 
          flex 
          items-center 
          space-x-3 
          bg-white 
          p-3 
          hover:bg-neutral-100
          rounded-lg
          transition
          cursor-pointer
        "
      >
        <Avatar user={data} />
        <div className="min-w-0 flex-1">
          <div className="focus:outline-none">
            <span className="absolute inset-0" aria-hidden="true" />
            <div className="flex justify-between items-center mb-1">
              <p className="text-sm font-medium text-gray-900">
                {data.name}
              </p>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}