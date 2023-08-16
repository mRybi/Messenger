import { FC } from "react";
import { UserInfo } from "../services/auth/authModels";
// import { useActiveList } from "../hooks";

type AvatarProps = {
  user?: UserInfo;
};

export const Avatar: FC<AvatarProps> = ({ user }) => {
//   const { members } = useActiveList();
  const isActive = true; // members.indexOf(user?.email!) !== -1;

  return (
    <div className="relative">
      <div className="
        relative 
        inline-block 
        rounded-full 
        overflow-hidden
        h-9 
        w-9 
        md:h-11 
        md:w-11
      ">
        <img
          src={'/images/placeholder.jpg'} // src={user?.image || '/images/placeholder.jpg'} ///dodac zapisywanie zdejc jakos
          alt="Avatar"
        />
      </div>
      {isActive ? (
        <span 
          className="
            absolute 
            block 
            rounded-full 
            bg-green-500 
            ring-2 
            ring-white 
            top-0 
            right-0
            h-2 
            w-2 
            md:h-3 
            md:w-3
          " 
        />
      ) : null}
    </div>
  );
}